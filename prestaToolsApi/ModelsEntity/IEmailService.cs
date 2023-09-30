namespace prestaToolsApi.ModelsEntity
{
    public interface IEmailService
    {
        Task SendAsyncronousEmail(string email, string subject, string message);

    }
}
