using CoreApp;

namespace Testing;

internal class Program
{
    private static void Main(string[] args)
    {
        var smsSender = new TwilioOTP();

        // Reemplaza con el número de teléfono del destinatario y el mensaje que quieres enviar
        var toPhoneNumber = "+50685663503";

        smsSender.SendMessage(toPhoneNumber);
    }
}