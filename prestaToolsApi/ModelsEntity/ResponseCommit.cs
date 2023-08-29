using Azure;
using Org.BouncyCastle.Utilities;
using System.Text.RegularExpressions;

namespace prestaToolsApi.ModelsEntity
{
    public class ResponseCommit
    {
        public string Vci { set; get; }
        //Resultado de la autenticación del tarjetahabiente.
        //Algunos de ellos son:
        //TSY - Autenticación Exitosa
        //TSN - Autenticación Rechazada
        //NP - No Participa, sin autenticación
        //U3 - Falla conexión, Autenticación Rechazada
        //INV - Datos InválidosA - Intentó
        //CNP1 - Comercio no participa
        //EOP - Error operacional
        //BNA - BIN no adherido
        //ENA - Emisor no adherido
        public decimal Amount { set; get; }
        public string Status { set; get; }
        //Estado de la transacción(INITIALIZED, AUTHORIZED, REVERSED, FAILED, NULLIFIED, PARTIALLY_NULLIFIED, CAPTURED).
        public string BuyOrder { set; get; } //
        //Orden de compra de la tienda indicado en Transaction.create(). Largo máximo: 26
        public string SessionId { set; get; }//
        //Identificador de sesión, el mismo enviado originalmente por el comercio en Transaction.create(). Largo máximo: 61.
        public string CardDetail { set; get; }
        //Objeto que representa los datos de la tarjeta de crédito del tarjeta habiente.
        //card_detail.card_number=String
        //4 últimos números de la tarjeta de crédito del tarjetahabiente.
        //Solo para comercios autorizados por Transbank se envía el número completo. Largo máximo: 19.
        public string AccountingDate { set; get; }
        //Fecha de la autorización. Largo: 4, formato MMDD
        public string TransactionDate { set; get; }
        //Fecha y hora de la autorización. Largo: 24, formato: ISO 8601 (Ej: yyyy-mm-ddTHH:mm:ss.xxxZ)
        public string AuthorizationCode { set; get; }
        public string PaymentTypeCode { set; get; }//
        //Tipo de pago de la transacción.
        //VD = Venta Débito.
        //VN = Venta Normal.
        //VC = Venta en cuotas.
        //SI = 3 cuotas sin interés.
        //S2 = 2 cuotas sin interés.
        //NC = N Cuotas sin interés
        //VP = Venta Prepago.
        public int ResponseCode { set; get; }
        //Código de respuesta de la autorización. Valores posibles:
        //0 = Transacción aprobada
        public decimal InstallmentsAmount { set; get; }
        //Monto de las cuotas. Largo máximo: 17
        public int InstallmentsNumber { set; get; }
        //Cantidad de cuotas. Largo máximo: 2
        public decimal Balance { set; get; }
        //Monto restante para un detalle anulado. Largo máximo: 17

    }
}
