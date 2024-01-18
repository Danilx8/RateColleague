namespace RateColleague.Services.EmailSenderService
{
    public interface IEmailSenderService
    {
        public void SendEmailAsync(string userEmailAddress, string resultingPdfLocation);
    }
}
