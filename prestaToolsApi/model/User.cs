namespace prestaToolsApi
{
    public class User
    {

        public int id { get; set; }
        public string name { get; set; }
   
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public string phone { get; set; }

        public string adress { get; set; }
        public string city { get; set; }
        public string country { get; set; }

        public string zipcode { get; set; }

        public string date { get; set; }

        public DateTime created_at { get; set; }
   
        public bool state { get; set;}

        public string d_identidad { get; set; }

    }
}
