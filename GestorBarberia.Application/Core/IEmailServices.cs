using GestorBarberia.Domain.Entities;
using GestorBarberia.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBarberia.Application.Core
{
    public interface IEmailServices
    {
        void SendEmail(string to, string subject, string body, bool isHtml = false);
        string RenderTemplate(string templateName, EmailModel model);
        string LoadEmbeddedTemplate(string templateName);
        EmailModel GenerateEmailModel(string NombreCliente, string Estado, DateTime FechaCita, TimeSpan HoraCita, string? NombreBarbero = null);
    }
}
