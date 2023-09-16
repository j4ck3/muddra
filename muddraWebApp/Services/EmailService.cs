using muddraWebApp.Models.ViewModels;
using System.Net;
using System.Net.Mail;

namespace muddraWebApp.Services;

public class EmailService
{
    public async Task SendEmailAsync(ContactViewModel viewModel)
    {
       var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        try
        {
            var smtpSettingsSection = configuration.GetSection("SmtpSettings");
            var host = smtpSettingsSection.GetValue<string>("Host");
            var port = smtpSettingsSection.GetValue<int>("Port");
            var userName = smtpSettingsSection.GetValue<string>("RelayUsername");
            var key = smtpSettingsSection.GetValue<string>("RelayKey");
            var reciver1 = smtpSettingsSection.GetValue<string>("Reciver1");
            var reciver2 = smtpSettingsSection.GetValue<string>("Reciver2");
            var sender = "noreply@muddra.com";

            var recipients = new List<string>
            {
                reciver1, reciver2
            };

            using var client = new SmtpClient(host, port);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(userName, key);

            var subject = "Nytt meddelande från " + viewModel.Email;

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