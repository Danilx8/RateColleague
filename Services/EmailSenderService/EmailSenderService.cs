using MailKit.Net.Smtp;
using MimeKit;

namespace RateColleague.Services.EmailSenderService
{
    public class EmailSenderService : IEmailSenderService
    {
        public void SendEmailAsync(string userEmailAddress, string resultingPdfLocation = "")
        {
            using MimeMessage emailMessage = new();

            emailMessage.From.Add(new MailboxAddress("RateColleague", "lugovskihdanil@yandex.ru"));
            emailMessage.To.Add(new MailboxAddress("", userEmailAddress));
            emailMessage.Subject = "RateColleague проект";

            BodyBuilder builder = new()
            {
                TextBody = @"Тестовое сообщение со случайным файлом"
            };
            //builder.Attachments.Add(resultingPdfLocation);

            emailMessage.Body = builder.ToMessageBody();

            using SmtpClient client = new();
            client.ConnectAsync("smtp.yandex.ru", 25, false);
            client.Authenticate("lugovskihdanil@yandex.ru", "password");
            client.Send(emailMessage);
            client.Disconnect(true);
        }
    }
}
