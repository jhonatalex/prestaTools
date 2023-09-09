using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Options;
using prestaToolsApi.ModelsEntity;
using Transbank.Common;
using Transbank.Webpay.Common;
using Transbank.Webpay.WebpayPlus;

namespace prestaToolsApi.Data.Repository
{
    
    public class VentaRepository : IVentaRepository
    {
        private readonly PrestatoolsContext _context;
        public VentaRepository(PrestatoolsContext context)
        {
            _context = context;
        }



        //Declaración de variables para uso del ApiResponse
        string token = "tu_token";
        bool success;
        ErrorRes errorRes = new ErrorRes();
        string message;

        public async Task<ApiResponse<ResponseTransaction>> iniciar(PayData payData)
        {

            // TO DO: insertar venta y detalle de venta en BDD. Antes de llamar a transbank

            try
            {
                success = true;
                message = "Transacción iniciada correctamente";

                // venta.Date = DateTime.Now.ToString("yyyy-MM-dd");
                // _context.Venta.Add(venta);
                // int result = await _context.SaveChangesAsync();
                // Versión 4.x del SDK
            
                var tx = new Transaction(new Options(IntegrationCommerceCodes.WEBPAY_PLUS, IntegrationApiKeys.WEBPAY, WebpayIntegrationType.Test));
                var responseTx =  tx.Create(payData.buyOrder, payData.sessionId, payData.amount, payData.returnUrl);
                var tokenTransbank = responseTx.Token;
                var UrlTranssbank = responseTx.Url;

                ResponseTransaction responseTransaction = new ResponseTransaction();
                responseTransaction.Token = tokenTransbank;
                responseTransaction.Url = UrlTranssbank;

                if (responseTx != null)
                {
                    //Guardar venta
                    _context.Venta.Add(payData.ventum);
                    var resultVentum = await _context.SaveChangesAsync();

                    //Mapear ID de venta
                    int nuevoID = (int)payData.ventum.IdVenta;
                    payData.detalleVentum.IdVenta = nuevoID;

                    //Guardar token transbank en detalle de venta
                    payData.detalleVentum.Token = tokenTransbank;

                    //Agregar detalle de venta
                    _context.DetalleVenta.Add(payData.detalleVentum);
                    var resultDetalle = await _context.SaveChangesAsync();
                    
                    //Armar la respuesta
                    var response = new ApiResponse<ResponseTransaction>(responseTransaction, token, success, errorRes, message);

                    return response;
                }
                else
                {
                    
                    success = false;
                    message = "Error al iniciar la transaccion";

                    var response = new ApiResponse<ResponseTransaction>(null, token, success, errorRes, message);
                    return response;
                } 


            }
            catch (Exception ex)
            {

                success = false;
                message = "Error";
                errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };
                var response = new ApiResponse<ResponseTransaction>(null, token, success, errorRes, message);
                return response;
            }

        }

        public async Task<ApiResponse<DetalleVentum>> insertar(DetalleVentum detalleVenta)
        {
            try
            {

                _context.DetalleVenta.Add(detalleVenta);
                var resultdetalle = await _context.SaveChangesAsync();

                if (resultdetalle == 1)
                {
                    success = true;
                    message = "Datos insertados correctamente";
                    
                }
                else
                {
                    success = false;
                    errorRes = new ErrorRes { code = resultdetalle, message = "Error al insertar al contexto" }; 
                    message = "Error al insertar";
                    
                }

                var response = new ApiResponse<DetalleVentum>(null, token, success, errorRes, message);
                return response;


            }
            catch (Exception ex)
            {

                token = "n/a";
                success = false;
                errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };
                message = "Error al insertar venta y/o detalle de venta";

                var response = new ApiResponse<DetalleVentum>(null, token, success, errorRes, message);
                return response;

            }
            
        }

        public async Task<ApiResponse<Ventum>> confirmar(Token tokenPasarela)
        {

            try
            {
                
                var tx = new Transaction(new Options(IntegrationCommerceCodes.WEBPAY_PLUS, IntegrationApiKeys.WEBPAY, WebpayIntegrationType.Test));
                var responseCx = tx.Commit(tokenPasarela.token);

                ResponseCommit responseCommit = new ResponseCommit();
                responseCommit.ResponseCode = (int)responseCx.ResponseCode;
                responseCommit.Status = responseCx.Status;


                if (responseCommit.ResponseCode == 0 && responseCommit.Status == "AUTHORIZED")
                {

                    //1. Buscar con el token el detalle de venta
                    var detalleVentaEncontrado = _context.DetalleVenta.FirstOrDefault(detalle => detalle.Token == tokenPasarela.token);
                    detalleVentaEncontrado = _context.DetalleVenta
                        .Include(c => c.IdToolNavigation)
                        .Where(ObjetoTool => ObjetoTool.IdTool == detalleVentaEncontrado.IdTool).FirstOrDefault();

                    //2. actualizar los nuevos valores (byorder, ssesionid, etc....)
                    detalleVentaEncontrado.BuyOrder = responseCommit.BuyOrder;
                    detalleVentaEncontrado.SessionId = responseCommit.SessionId;
                    detalleVentaEncontrado.PaymentTypeCode = responseCommit.PaymentTypeCode;
                    detalleVentaEncontrado.InstallmentsAmount = responseCommit.InstallmentsAmount;
                    detalleVentaEncontrado.InstallmentsNumber = responseCommit.InstallmentsNumber;
                    _context.DetalleVenta.Update(detalleVentaEncontrado);
                    int resultdetalle = await _context.SaveChangesAsync();

                    //3. buscar la venta con el ID de venta
                    var ventaEncontrada = _context.Venta.FirstOrDefault(venta => venta.IdVenta == detalleVentaEncontrado.IdVenta);

                    //4. cambiar el state a la venta a "Pagado"
                    ventaEncontrada.State = "Pagada";
                    _context.Venta.Update(ventaEncontrada);
                    int resultventa = await _context.SaveChangesAsync();

                    //5. enviar al frond la venta y detalle de venta (como 2 objetos)
                    success = true;
                    message = "Transacción confirmada";
                    var response = new ApiResponse<Ventum>(ventaEncontrada, token, success, errorRes, message);
                    return response;

                }
                else
                {
                    //To do:
                    //1. Buscar con el token el detalle de venta
                    //2. buscar la venta con el ID de venta
                    //3. cambiar el state a la venta a "Rechazada"
                    //4. enviar al frond la venta y detalle de venta (como 2 objetos)

                    success = false;
                    message = "Error al confirmar transaccion";
                    var response = new ApiResponse<Ventum>(null, token, success, errorRes, message);
                    return response;
                }


            }
            catch (Exception ex)
            {

                success = false;
                message = "Error";
                errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };
                var response = new ApiResponse<Ventum>(null, token, success, errorRes, message);
                return response;
            }

        }

    }
}
