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
                    _context.Venta.Add(payData.ventum);
                    var resultVentum = await _context.SaveChangesAsync();

                    int nuevoID = (int)payData.ventum.IdVenta;
                    payData.detalleVentum.IdVenta = nuevoID;

                    _context.DetalleVenta.Add(payData.detalleVentum);
                    var resultDetalle = await _context.SaveChangesAsync();
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

                //_context.Venta.Add(venta);
                //var result = await _context.SaveChangesAsync();
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

        public async Task<ApiResponse<ResponseCommit>> confirmar(string tokenPasarela)
        {

            try
            {
                
                var tx = new Transaction(new Options(IntegrationCommerceCodes.WEBPAY_PLUS, IntegrationApiKeys.WEBPAY, WebpayIntegrationType.Test));
                var responseCx = tx.Commit(tokenPasarela);

                ResponseCommit responseCommit = new ResponseCommit();
                responseCommit.ResponseCode = (int)responseCx.ResponseCode;
                responseCommit.Status = responseCx.Status;

                if (responseCommit.ResponseCode == 0 && responseCommit.Status == "AUTHORIZED")
                {

                    success = true;
                    message = "Transacción confirmada";
                    //Actualizar tabla: marcar venta como paga
                    var response = new ApiResponse<ResponseCommit>(responseCommit, token, success, errorRes, message);
                    return response;
                }
                else
                {

                    success = false;
                    message = "Error al confirmar transaccion";
                    var response = new ApiResponse<ResponseCommit>(null, token, success, errorRes, message);
                    return response;
                }


            }
            catch (Exception ex)
            {

                success = false;
                message = "Error";
                errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };
                var response = new ApiResponse<ResponseCommit>(null, token, success, errorRes, message);
                return response;
            }

        }

    }
}
