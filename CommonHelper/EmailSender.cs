using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gharshare.CommonHelper
{
    public class EmailSender : IEmailSender
    {
        public byte[] Attachment { get; set; }
        public string? FileName  { get;set; }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                //Reading smtp setting from appsettings.json file

                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.Json", optional: false);
                IConfiguration configuration = builder.Build();
                string host = configuration.GetValue<string>("Smtp:Host");
                int port = Convert.ToInt32(configuration.GetValue<string>("Smtp:Port"));
                string fromAddress = configuration.GetValue<string>("Smtp:FromAddress");
                string userName = configuration.GetValue<string>("Smtp:UserName");
                string password = configuration.GetValue<string>("Smtp:Password");
                string cc = configuration.GetValue<string>("Smtp:cc");
                string bcc = configuration.GetValue<string>("Smtp:bcc");

                // send email
                var message = new MimeMessage();
                var bodyBuilder = new BodyBuilder();// newly added, not tested yet


                message.From.Add(MailboxAddress.Parse(fromAddress));
                message.To.Add(MailboxAddress.Parse(email));
                if (cc != null)
                    message.Cc.Add(MailboxAddress.Parse(cc));
                if (bcc != null)
                    message.Bcc.Add(MailboxAddress.Parse(bcc));
                message.Subject = subject;
                // _toEmail.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };
                bodyBuilder.HtmlBody = htmlMessage; //newly added
                if(Attachment !=null)
                {
                    bodyBuilder.Attachments.Add(FileName, Attachment);//newly added
                }
                
                message.Body = bodyBuilder.ToMessageBody();

                using (var _emailClient = new SmtpClient())
                {
                    _emailClient.Connect(host, port, MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
                    //above setting of StartTls is for test smtp server.
                    //_emailClient.Connect(host, port, MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
                    _emailClient.Authenticate(userName, password);//commented for test server that does not support auth
                    _emailClient.Send(message);
                    _emailClient.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                //TempData["error"] = ex.Message;
            }
            return Task.CompletedTask;

        }
        
    }
}
