using muddraWebApp.Models.ViewModels;
using System.Net;
using System.Net.Mail;

namespace muddraWebApp.Services;

public class EmailService
{
    public async Task SendEmailAsync(ContactViewModel viewModel)
    {
        DotNetEnv.Env.Load();

        try
        {

            string host = Environment.GetEnvironmentVariable("HOST")!;
            int port = DotNetEnv.Env.GetInt("PORT");
            string userName = Environment.GetEnvironmentVariable("SMTP_USERNAME")!;
            string key = Environment.GetEnvironmentVariable("SMTP_KEY")!;
            string reciver1 = Environment.GetEnvironmentVariable("RECIVER_1")!;
            string reciver2 = Environment.GetEnvironmentVariable("RECIVER_2")!;
            string sender = "noreply@muddra.com";

            var recipients = new List<string>
            {
                reciver1!, reciver2!
            };

            using var client = new SmtpClient(host, port);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(userName, key);

            var subject = "Meddelande från " + viewModel.Name;

            var body = viewModel.Name + Environment.NewLine + viewModel.Message + Environment.NewLine + viewModel.Area;

            foreach (var recipient in recipients)
            {
                var mailMessage = new MailMessage(from: sender, to: recipient, subject, body);
                await client.SendMailAsync(mailMessage);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
            throw;
        }
    }
}