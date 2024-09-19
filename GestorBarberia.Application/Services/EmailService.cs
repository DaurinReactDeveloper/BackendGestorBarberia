using GestorBarberia.Application.Core;
using GestorBarberia.Infrastructure.Models;
using Scriban;
using System.Net.Mail;
using System.Net;
using System.Reflection;

public class EmailService : IEmailServices
{
    private readonly SmtpClient _smtpClient;
    private readonly string _fromAddress;
    private readonly Assembly _assembly;

    public EmailService(string host, int port, bool enableSsl, string userName, string appPassword, string fromAddress)
    {
        _smtpClient = new SmtpClient(host, port)
        {
            EnableSsl = enableSsl,
            Credentials = new NetworkCredential(userName, appPassword)
        };
        _fromAddress = fromAddress;
        _assembly = Assembly.GetExecutingAssembly();  
    }

    public EmailModel GenerateEmailModel(string NombreCliente, string Estado, DateTime FechaCita, TimeSpan Horacita, string? NombreBarbero = null)
    {
        EmailModel model = new EmailModel()
        {
            NombreCliente = NombreCliente,
            Estado = Estado,
            FechaCita = FechaCita.ToShortDateString(),
            HoraCita = Horacita.ToString(@"hh\:mm"),
            NombreBarbero = NombreBarbero
        };

        return model;
    }
    // Métodos para cargar la plantilla embebida
    public string LoadEmbeddedTemplate(string templateName)
    {
        try
        {
            var resourceName = $"GestorBarberia.Application.EmailTemplates.{templateName}.html";
            using (var stream = _assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException($"La plantilla {templateName} no fue encontrada como recurso embebido.");
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al cargar la plantilla embebida: {ex.Message}");
            return string.Empty;
        }
    }
    public string RenderTemplate(string templateName, EmailModel model)
    {
        try
        {
            // Cargar la plantilla embebida
            var templateContent = LoadEmbeddedTemplate(templateName);

            if (string.IsNullOrEmpty(templateContent))
            {
                throw new Exception("El contenido de la plantilla está vacío.");
            }

            // Crear un ScriptObject para Scriban
            var scriptObject = new Scriban.Runtime.ScriptObject();
            scriptObject.Add("NombreCliente", model.NombreCliente);
            scriptObject.Add("Estado", model.Estado);
            scriptObject.Add("FechaCita", model.FechaCita);
            scriptObject.Add("HoraCita", model.HoraCita);

            // Agregar NombreBarbero si no es nulo
            if (!string.IsNullOrEmpty(model.NombreBarbero))
            {
                scriptObject.Add("NombreBarbero", model.NombreBarbero);
            }

            // Usar Scriban para renderizar la plantilla
            var template = Template.Parse(templateContent);
            var renderedContent = template.Render(scriptObject);

            return renderedContent;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al renderizar la plantilla: {ex.Message}.");
            return string.Empty;
        }
    }
    public void SendEmail(string to, string subject, string body, bool isHtml = true)
    {
        try
        {
            var mailMessage = new MailMessage(_fromAddress, to, subject, body)
            {
                IsBodyHtml = isHtml
            };

            _smtpClient.Send(mailMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al enviar el correo: {ex.Message}.");
        }
    }
  
}
