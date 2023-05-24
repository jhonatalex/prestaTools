using Microsoft.AspNetCore.Components.Web;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Utilities;

namespace prestaToolsApi.model
{
    public class Tool
    {
        public int id { set; get; }
        public string name { set; get; }
        public string reference { set; get; }
        public bool nuevo {set ; get; }
        public string model { set; get; }
        public string description { set; get; }
        public bool widgets { set; get; }
        public double value_commercial { set; get; }
        public double value_rent { set; get; }
        public int year_buy { set; get; }
        public double weigt { set; get; }
        public double mesuare { set; get; }
        public int number_piece { set; get; }
        public string url_image_1 { set; get; }
        public string url_image_2 { set; get; }
        public string url_image_3 { set; get; }
        public string terms_use { set; get; }
        public string break_downs { set; get; }
        public double time_use { set; get; }
        public int id_category { set; get; }
        public int id_user { set; get; }
        public int id_lenders { set; get; }
        public int date_up { set; get; }


    }
}
