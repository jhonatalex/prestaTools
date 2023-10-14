using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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

        private readonly IEmailService _emailService;

        public VentaRepository(PrestatoolsContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService; 
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
                    message = "Error al iniciar venta (if)";

                    var response = new ApiResponse<ResponseTransaction>(null, token, success, errorRes, message);
                    return response;
                } 


            }
            catch (Exception ex)
            {

                success = false;
                message = "Error al iniciar venta (catch)";
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
                    message = "Error al insertar venta (if)";
                    
                }

                var response = new ApiResponse<DetalleVentum>(null, token, success, errorRes, message);
                return response;


            }
            catch (Exception ex)
            {

                token = "n/a";
                success = false;
                errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };
                message = "Error al insertar venta (catch)";

                var response = new ApiResponse<DetalleVentum>(null, token, success, errorRes, message);
                return response;

            }
            
        }

        public async Task<ApiResponse<Ventum>> confirmar(Token tokenPasarela)
        {
            String rastroError = "";
            try
            {
                rastroError = "1";
                var tx = new Transaction(new Options(IntegrationCommerceCodes.WEBPAY_PLUS, IntegrationApiKeys.WEBPAY, WebpayIntegrationType.Test));
                rastroError = rastroError + "2";
                var responseCx = tx.Commit(tokenPasarela.token);
                rastroError = rastroError + "3";

                ResponseCommit responseCommit = new ResponseCommit();
                rastroError = rastroError + "4";
                responseCommit.ResponseCode = (int)responseCx.ResponseCode;
                rastroError = rastroError + "5";
                responseCommit.Status = responseCx.Status;
                rastroError = rastroError + "6";


                if (responseCommit.ResponseCode == 0 && responseCommit.Status == "AUTHORIZED")
                {
                    rastroError = rastroError + "7";
                    //1. Buscar con el token el detalle de venta
                    var detalleVentaEncontrado = _context.DetalleVenta.FirstOrDefault(d => d.Token == tokenPasarela.token);
                    rastroError = rastroError + "8" + " - " + detalleVentaEncontrado.Token + " - " + detalleVentaEncontrado.IdTool + " - " + detalleVentaEncontrado.IdToolNavigation;
                    //detalleVentaEncontrado = _context.DetalleVenta
                    //    .Include(c => c.IdToolNavigation)
                    //    .Where(t => t.IdTool == detalleVentaEncontrado.IdTool).FirstOrDefault();

                    // conseguir la tool rentada
                    var toolRentada = _context.Tools.FirstOrDefault(t => t.Id == detalleVentaEncontrado.IdTool);

                    rastroError = rastroError + "9";

                    //2. actualizar los nuevos valores (byorder, ssesionid, etc....)
                    detalleVentaEncontrado.BuyOrder = responseCommit.BuyOrder;
                    rastroError = rastroError + "a";
                    detalleVentaEncontrado.SessionId = responseCommit.SessionId;
                    rastroError = rastroError + "b";
                    detalleVentaEncontrado.PaymentTypeCode = responseCommit.PaymentTypeCode;
                    rastroError = rastroError + "c";
                    detalleVentaEncontrado.InstallmentsAmount = responseCommit.InstallmentsAmount;
                    rastroError = rastroError + "d";
                    detalleVentaEncontrado.InstallmentsNumber = responseCommit.InstallmentsNumber;
                    rastroError = rastroError + "e";
                    _context.DetalleVenta.Update(detalleVentaEncontrado);
                    rastroError = rastroError + "f";
                    int resultdetalle = await _context.SaveChangesAsync();
                    rastroError = rastroError + "g";

                    //3. buscar la venta con el ID de venta
                    var ventaEncontrada = _context.Venta.FirstOrDefault(venta => venta.IdVenta == detalleVentaEncontrado.IdVenta);
                    rastroError = rastroError + "h";

                    //4. cambiar el state a la venta a "Pagado"
                    ventaEncontrada.State = "Pagada";
                    rastroError = rastroError + "i";
                    _context.Venta.Update(ventaEncontrada);
                    rastroError = rastroError + "j";
                    int resultventa = await _context.SaveChangesAsync();
                    rastroError = rastroError + "k";

                    // cambiar el estado de la tool a rentado
                    toolRentada.State = "rentado";
                    _context.Tools.Update(toolRentada);
                    int result = await _context.SaveChangesAsync();
                    rastroError = rastroError + "l";

                    // encontrar user y demás para hacer la tabla

                    var estaVenta = _context.Venta.FirstOrDefault(v => v.IdVenta == detalleVentaEncontrado.IdVenta);
                    var esteUser = _context.Users.FirstOrDefault(u => u.Email == estaVenta.IdUser); 

                    //5. Enviar un email al usuario: detalle de venta y herramienta que alquiló

                    EmailDTO emailObject = new EmailDTO();
                     
                    emailObject.Asunto = "Hemos Recibido tu pedido en PrestaTools";
                    var contenido = $" <h1>Gracias por tu pedido</h1>\r\n    <p>Hola {esteUser.Name} {esteUser.LastName},</p>\r\n    " +
                          $"<p>Solo para que lo sepas, hemos recibido tu pedido {estaVenta.NumberComprobante}, y ahora se está procesando:</p>\r\n  " +

                          $"<table cellspacing=\"0\" cellpadding=\"6\" border=\"1\" style=\"color:#636363;border:1px solid #e5e5e5;vertical-align:middle;width:100%;font-family:" +
                          $"'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif\" width=\"100%\">\r\n\t\t<thead>\r\n\t\t\t<tr>\r\n\t\t\t\t<th scope=\"col\" style=\"color:#636363;" +
                          $"border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left\" align=\"left\">Producto</th>\r\n\t\t\t\t<th scope=\"col\" style=\"color:#636363;border:1px solid #e5e5e5;" +
                          $"vertical-align:middle;padding:12px;text-align:left\" align=\"left\">Cantidad</th>\r\n\t\t\t\t<th scope=\"col\" style=\"color:#636363;border:1px solid #e5e5e5;vertical-align:middle;" +
                          $"padding:12px;text-align:left\" align=\"left\">Precio</th>\r\n\t\t\t</tr>\r\n\t\t</thead>\r\n\t\t<tbody>\r\n\t\t\t\t<tr>\r\n\t\t<td style=\"color:#636363;border:1px solid #e5e5e5;" +
                          $"padding:12px;text-align:left;vertical-align:middle;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif;word-wrap:break-word\" align=\"left\">{toolRentada.Name}</td>" +
                          $"\r\n\t\t<td style=\"color:#636363;border:1px solid #e5e5e5;padding:12px;text-align:left;vertical-align:middle;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif\" align=\"left\">" +
                          $"1</td>\r\n\t\t<td style=\"color:#636363;border:1px solid #e5e5e5;padding:12px;text-align:left;vertical-align:middle;font-family:'Helvetica Neue',Helvetica,Roboto,Arial,sans-serif\" align=\"left\">" +
                          $"\r\n\t\t\t<span><span>$</span>{detalleVentaEncontrado.Price}</span>\t\t</td>\r\n\t</tr>\r\n\t\r\n\t\t</tbody>\r\n\t\t<tfoot>\r\n\t\t\t\t\t\t\t\t<tr>\r\n\t\t\t\t\t\t<th scope=\"row\" colspan=\"2\" style=\"color:#636363;" +
                          $"border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left;border-top-width:4px\" align=\"left\">Subtotal:</th>\r\n\t\t\t\t\t\t<td style=\"color:#636363;border:1px solid #e5e5e5;" +
                          $"vertical-align:middle;padding:12px;text-align:left;border-top-width:4px\" align=\"left\"><span><span>$</span>{detalleVentaEncontrado.Total}</span></td>\r\n\t\t\t\t\t</tr>\r\n\t\t\t\t\t\t\t\t\t\t<tr>" +
                          $"\r\n\t\t\t\t\t\t<th scope=\"row\" colspan=\"2\" style=\"color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left\" align=\"left\">Método de pago:</th>" +
                          $"\r\n\t\t\t\t\t\t<td style=\"color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left\" align=\"left\">{detalleVentaEncontrado.PaymentTypeCode}</td>\r\n\t\t\t\t\t</tr>" +
                          $"\r\n\t\t\t\t\t\t\t\t\t\t<tr>\r\n\t\t\t\t\t\t<th scope=\"row\" colspan=\"2\" style=\"color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left\" align=\"left\">Total:</th>" +
                          $"\r\n\t\t\t\t\t\t<td style=\"color:#636363;border:1px solid #e5e5e5;vertical-align:middle;padding:12px;text-align:left\" align=\"left\"><span><span>$</span>{detalleVentaEncontrado.Total}</span></td>\r\n\t\t\t\t\t</tr>" +
                          $"\r\n\t\t\t\t\t\t\t</tfoot>\r\n\t</table>" +

                          $"<p>Dirección de facturación:</p>\r\n    <address>\r\n  " +
                          $"      {esteUser.Email}<br>" +
                          $"      {esteUser.Address}<br>" +
                          $"      {esteUser.region}<br>" +
                          $"      {esteUser.commune}<br>" +
                          $"      {esteUser.Telephone}<br>" +
                          $"   </address>\r\n    \r\n    " +
                          $"<p>¡Gracias por usar PrestaTools.cl!</p>" +
                          $"<p>ventas@prestatools.clls.cl</p>";
                    //$"< a href =\"mailto:ventas@prestatools.cl\">ventas@prestatools.cl</a>";

                    emailObject.Contenido = contenido;
                    emailObject.Para = ventaEncontrada.IdUser;

                    _emailService.SendEmail(emailObject);

                    //   y al lender: herramienta que alquiló y su comisión (guardarla en amount 60% del total)

                    EmailDTO emailObjectLender = new EmailDTO();

                    var contenidoLender = "<table border=\"1\" style=\"border-collapse: collapse; width: 100%; max-width: 800px; margin: 0 auto;\">" +
                        "\r\n        <thead>\r\n            <tr>\r\n                <th style=\"background-color: #329cfa; color: white; padding: 12px; text-align: left;\">" +
                        "Producto</th>\r\n                <th style=\"background-color: #329cfa; color: white; padding: 12px; text-align: left;\">Cantidad de Días</th>" +
                        "\r\n                <th style=\"background-color: #329cfa; color: white; padding: 12px; text-align: left;\">Rentado Desde</th>" +
                        "\r\n                <th style=\"background-color: #329cfa; color: white; padding: 12px; text-align: left;\">Rentado Hasta</th>" +
                        "\r\n                <th style=\"background-color: #329cfa; color: white; padding: 12px; text-align: left;\">Comisión</th>" +
                        "\r\n                <th style=\"background-color: #329cfa; color: white; padding: 12px; text-align: left;\">Número de Orden</th>" +
                        "\r\n                <th style=\"background-color: #329cfa; color: white; padding: 12px; text-align: left;\">Dirección de Envío</th>" +
                        $"\r\n            </tr>\r\n        </thead>\r\n        <tbody>\r\n            <tr>\r\n                <td style=\"padding: 12px; text-align: left;\">detalleVentaEncontrado.IdToolNavigation.Name</td>" +
                        "\r\n                <td style=\"padding: 12px; text-align: left;\">5</td>\r\n                <td style=\"padding: 12px; text-align: left;\">2023-09-01</td>" +
                        "\r\n                <td style=\"padding: 12px; text-align: left;\">2023-09-06</td>\r\n                <td style=\"padding: 12px; text-align: left;\">$50.00</td>" +
                        "\r\n                <td style=\"padding: 12px; text-align: left;\">12345</td>\r\n                <td style=\"padding: 12px; text-align: left;\">" +
                        "123 Calle Principal, Ciudad</td>\r\n            </tr>\r\n        </tbody>\r\n    </table>";

                    //   y al administrador de la página: detalle de venta con comisión y todo

                    //6. enviar al frond la venta y detalle de venta (como 2 objetos)
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
                    message = "Error al confirmar venta (else)";
                    var response = new ApiResponse<Ventum>(null, token, success, errorRes, message);
                    return response;
                }


            }
            catch (Exception ex)
            {

                success = false;
                message = "Error al confirmar venta (catch)" + rastroError;
                errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };
                var response = new ApiResponse<Ventum>(null, token, success, errorRes, message);
                return response;
            }

        }

        public async Task<ApiResponse<Ventum>> sendEmail(EmailDTO emailObject)
        {
            try
            {
            

                var contenido = $" <h1>Gracias por tu pedido</h1>\r\n    <p>Hola Jhonat,</p>\r\n    " +
                    "<p>Solo para que lo sepas, hemos recibido tu pedido #3382, y ahora se está procesando:</p>\r\n  " +
                    "  \r\n    <table border=\"1\">\r\n        <tr>\r\n            <th>Pedido #3382 (19 de septiembre de 2023)</th>\r\n   " +
                    "         <th>Producto</th>\r\n            <th>Cantidad</th>\r\n            <th>Precio</th>\r\n        </tr>\r\n     " +
                    "   <tr>\r\n            <td rowspan=\"3\">Polera Go - M, Gris</td>\r\n            <td>Talla:</td>\r\n            <td>M</td>\r\n    " +
                    "        <td rowspan=\"3\">$15.000</td>\r\n        </tr>\r\n        <tr>\r\n            <td>Color:</td>\r\n            <td>Gris</td>\r\n  " +
                    "      </tr>\r\n        <tr>\r\n            <td>Subtotal:</td>\r\n            <td>$15.000</td>\r\n        </tr>\r\n    </table>\r\n    \r\n  " +
                    "  <p>Método de pago: Transbank Webpay Plus</p>\r\n    <p>Total: $15.000</p>\r\n    \r\n    <p>Dirección de facturación:</p>\r\n    <address>\r\n  " +
                    "      Market Global<br>\r\n        Jhonatan Mejias<br>\r\n        Diego Isidro Monardez<br>\r\n        Coquimbo<br>\r\n        1840000 Ovalle<br>\r\n " +
                    "       +56941623264<br>\r\n        <a href=\"mailto:jhonatanmejias@gmail.com\">jhonatanmejias@gmail.com</a>\r\n " +
                    "   </address>\r\n    \r\n    <p>¡Gracias por usar PrestaTools.cl!</p>";
                //_emailSender.SendAsyncronousEmail(email, subject, messageEmail);

                emailObject.Contenido= contenido;

                _emailService.SendEmail(emailObject);
                success = true;
                message = "Mensaje Enviado con Exito";
             
                var response = new ApiResponse<Ventum>(null, token, success, errorRes, message);
                return response;


            }
            catch (Exception ex)
            {

                token = "n/a";
                success = false;
                errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };
                message = "Error al enviar email (catch)";

                var response = new ApiResponse<Ventum>(null, token, success, errorRes, message);
                return response;

            }
        }
    }
}
