using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

public class SendGridEmail
{
    private readonly string _apiKey;

    public SendGridEmail()
    {
        _apiKey = "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";
    }
    //Se pueden crear mas templates del email :)
    public async Task SendEmailAsync(string toEmail, string password)
    {
        var client = new SendGridClient(_apiKey);
        var from = new EmailAddress("siluetaclubfitness@gmail.com", "Silueta Club Fitness");
        var to = new EmailAddress(toEmail);
        var subject = "Clave Temporal de Silueta Club Fitness";
        var plainTextContent = "Clave temporal enviada por Silueta Club Fitness";

        var htmlContent = "<h3>Se te ha enviado tu clave temporal, no olvides cambiarla.</h3>" + "<strong>" + "Clave temporal: " + password + "</strong>";

        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);
    }

    //Se quita el async y el await por problemas con el codigo.
    public void SendEmailAsyncPasswordChanges(string toEmail)
    {
        var client = new SendGridClient(_apiKey);
        var from = new EmailAddress("siluetaclubfitness@gmail.com", "Silueta Club Fitness");
        var to = new EmailAddress(toEmail);
        var subject = "Su clave ha sido cambiada";
        var plainTextContent = "Nueva clave registrada en Silueta Club Fitness";
        var htmlContent = "<h3>Se ha cambiado tu clave, si no fuiste tú por favor verifica.</h3>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

        var response = client.SendEmailAsync(msg);
    }
}

