using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Options;
using prestaToolsApi.ModelsEntity;
using Transbank.Common;
using Transbank.Webpay.Common;
using Transbank.Webpay.WebpayPlus;

namespace prestaToolsApi.Data.Repository
{
    
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PrestatoolsContext _context;

        //Declaración de variables para uso del ApiResponse
        string token = "tu_token";
        bool success;
        ErrorRes errorRes = new ErrorRes();
        string message;

        public async Task<ApiResponse<ResponseTransaction>> InsertarVenta(Ventum venta)
        {

            try
            {
                success = true;
                message = "Transacción iniciada correctamente";

                venta.Date = DateTime.Now.ToString("yyyy-MM-dd");

                _context.Venta.Add(venta);
                int result = await _context.SaveChangesAsync();

                // Versión 4.x del SDK
                //var tx = new Transaction(new Options(IntegrationCommerceCodes.WEBPAY_PLUS, IntegrationApiKeys.WEBPAY, WebpayIntegrationType.Test));
                //var responseTx = tx.Create(payData.buyOrder, payData.sessionId, payData.amount, payData.returnUrl);
                //Console.WriteLine(responseTx);
                
                var response2 = new ApiResponse<ResponseTransaction>(null, token, success, errorRes, message);
                return response2;

            }
            catch (Exception ex)
            {
                User user = null;
                success = false;
                message = "Error";
                var errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };

                var response = new ApiResponse<ResponseTransaction>(null, token, success, errorRes, message);
                return response;
            }

        }
    }
}
