using Azure;

namespace prestaToolsApi.ModelsEntity
{
    public class ResponseCommit
    {
        public string Vci { set; get; }
        public decimal Amount { set; get; }
        public string Status { set; get; }
        public string BuyOrder { set; get; }
        public string SessionId { set; get; }
        public string CardDetail { set; get; }
        public string AccountingDate { set; get; }
        public string TransactionDate { set; get; }
        public string AuthorizationCode { set; get; }
        public string PaymentTypeCode { set; get; }
        public int ResponseCode { set; get; }
        public decimal InstallmentsAmount { set; get; }
        public decimal InstallmentsNumber { set; get; }
        public decimal Balance { set; get; }

    }
}
