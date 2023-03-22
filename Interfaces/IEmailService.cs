namespace UploadFile.Interfaces;

public interface IEmailService
{
    public void SendEmailNotification(string email, string fileName);
}