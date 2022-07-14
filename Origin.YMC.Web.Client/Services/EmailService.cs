using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Origin.YMC.Web.Client.Services
{
    public class EmailService : IIdentityMessageService
    {
        #region Private Members
        private readonly string _from = ConfigurationManager.AppSettings["emailfrom"];
        private readonly string _password = ConfigurationManager.AppSettings["emailpassword"];
        private readonly string _host = ConfigurationManager.AppSettings["emailhost"];
        private readonly string _displayName = ConfigurationManager.AppSettings["emailfromdisplayname"];
        private readonly int _port = int.Parse(ConfigurationManager.AppSettings["emailport"]);


        #endregion

        public Task SendAsync(IdentityMessage message)
        {
            var mail = new MailMessage
            {
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8,
                From = new MailAddress(_from, _displayName)
            };

            mail.To.Add(message.Destination);
            var smtpClient = new SmtpClient
            {
                Host = _host,
                Port = _port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_from, _password),
                EnableSsl = true
            };

            return Task.Run(() => smtpClient.Send(mail));
        }
    }
}