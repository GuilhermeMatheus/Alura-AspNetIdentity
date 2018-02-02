using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace ByteBank.Forum.Identity
{
    public class EmailServico : IIdentityMessageService
    {
        private readonly string EMAIL_USUARIO = ConfigurationManager.AppSettings["emailService:usuario"];
        private readonly string EMAIL_SENHA = ConfigurationManager.AppSettings["emailService:senha"];

        public Task SendAsync(IdentityMessage message)
        {
            using (var mensagemDeEmail = new MailMessage())
            {
                mensagemDeEmail.From = new MailAddress(EMAIL_USUARIO);

                mensagemDeEmail.To.Add(message.Destination);
                mensagemDeEmail.Subject = message.Subject;
                mensagemDeEmail.Body = message.Body;

                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Host = "smtp.gmail.com";
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Credentials = new NetworkCredential(EMAIL_USUARIO, EMAIL_SENHA);
                    smtpClient.Timeout = 20000;

                    return smtpClient.SendMailAsync(mensagemDeEmail);
                }
            }
        }
    }
}