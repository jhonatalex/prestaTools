namespace prestaToolsApi.ModelsEntity
{
    public class ApiResponse<T>
    {

        public ApiResponse(T data, string _token, bool _success, ErrorRes _error, string _message) 
        {
            response = data;
            token = _token;
            success = _success;
            error = _error;
            message = _message;
        }

        public T response { get; set; }
        public string token { get; set; }
        public bool success { get; set; }
        public ErrorRes error { get; set; }
        public string message { get; set; }

    }
}
