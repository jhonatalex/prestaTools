namespace prestaToolsApi
{
    public class Lender
    {

        public int id { get; set; }
        public string d_identidad { get; set; }
        public string name { get; set; }
        public string last_name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
        public string address { get; set; }
        public string number_bank { get; set; }
        public double balance_wallet { get; set; }
        public DateTime date_up { get; set; }
        public bool state { get; set; }

    }
}
