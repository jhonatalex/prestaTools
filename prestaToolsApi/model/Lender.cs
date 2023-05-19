namespace prestaToolsApi.model
{
    public class Lender
    {

        public int id { get; set; }
        public string name { get; set; }
        public string d_identidad { get; set; }

        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public string phone { get; set; }

        public string addres { get; set; }
        public string city { get; set; }
        public string country { get; set; }

        public string number_bank { get; set; }

        public string balance_wallet { get; set; }

        public DateTime created_at { get; set; }

        public bool state { get; set; }

     



    }
}
