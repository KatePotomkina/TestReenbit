using System.Net;
using System.Net.Mail;
using UploadFile.Interfaces;

namespace UploadFile.Service;

public class EmailService : IEmailService
{
    public void SendEmailNotification(string email, string fileName)
    {
        // Create a new SMTP client
        using (var smtpClient = new SmtpClient("smtp.example.com", 587))
        {
            // Configure the SMTP client
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("ttestbest6@gmail.com", "SlavaUkraine12345");

            // Create a new email message
            using (var message = new MailMessage())
            {
                message.From = new MailAddress("ttestbest6@gmail.com");
                message.To.Add(new MailAddress(email));
                message.Subject = "File Uploaded Successfully";
                message.Body = $"Your file '{fileName}' has been uploaded successfully.";
                // Send the email message
                smtpClient.Send(message);
            }
        }
    }
}