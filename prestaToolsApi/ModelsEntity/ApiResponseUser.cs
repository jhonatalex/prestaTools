namespace prestaToolsApi.ModelsEntity
{
    public class ApiResponseUser

    {
      public string token { get; set; }
      public User user { get; set; }
      public bool success { get; set; }
      public ErrorRes  error { get; set; }

      public string message { get; set; }


    }
}
