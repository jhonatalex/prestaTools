namespace prestaToolsApi.ModelsEntity
{
    public interface IEmailSender
    {
        Task SendAsyncronousEmail(string email, string subject, string message);
    }
}
