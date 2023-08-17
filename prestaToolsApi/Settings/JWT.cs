namespace prestaToolsApi.Settings
{
    public class JWT
    {
        public string key { get; set; }
        public string issuer { get; set; }
        public string audience { get; set; }
        public string durationInMinutes { get; set; }
       
    }
}
