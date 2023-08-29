namespace prestaToolsApi.ModelsEntity
{
    public class PayData
    {
        public string buyOrder { get; set; }
        public string sessionId { get; set; }
        public decimal amount { get; set; }
        public string returnUrl { get; set; }
        public Ventum ventum { get; set; }
        public DetalleVentum detalleVentum { get; set; }

    }
}
