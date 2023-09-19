using MimeKit;
using MailKit.Net.Smtp;

namespace Lab_2.Services.Services
{
	public class EmailSender : IEmailSender
	{
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Your Name", "your@email.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                TextBody = message
            };

            emailMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.server.com", 587, false);
                await client.AuthenticateAsync("your@email.com", "your-password");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
        }
}
