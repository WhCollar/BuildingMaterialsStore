using System;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using RestSharp;
using RestSharp.Authenticators;
using TsentrstroyAPI.Model;

namespace TsentrstroyAPI.Services
{
    public class EmailNotificationService : INotificationService
    {
        private const string _fromName = "ООО ЦЕНТРСТРОЙ";
        private readonly string _fromAdress = "tsentrstroy72@uotlook-login-life.ru";
        
        private readonly string _subject;
        private readonly string _recipientAddress;
        private readonly string[] _body;

        private readonly StringBuilder _stringBuilder = new StringBuilder();
        
        public EmailNotificationService(string recipientAddress, string subject, params string[] content)
        {
            _recipientAddress = recipientAddress;
            _subject = subject;
            _body = content;
        }

        public async Task Send()
        {
            MimeMessage emailMessage = new MimeMessage();
 
            emailMessage.From.Add(new MailboxAddress(_fromName, _fromAdress));
            emailMessage.To.Add(new MailboxAddress("", _recipientAddress));
            emailMessage.Subject = _subject;
            
            emailMessage.Body = new TextPart(TextFormat.Html) { Text = BuildMessageContent()};
             
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.msndr.net", 587, false);
                await client.AuthenticateAsync("pasharudnykh3@mail.ru", "c317c16c8c1424011a705599f6135491");
                await client.SendAsync(emailMessage);
 
                await client.DisconnectAsync(true);
            }
        }

        private string BuildMessageContent()
        {
            for (int i = 0; i < _body.Length; i++)
                _stringBuilder.Append(_body[i]);

            return _stringBuilder.ToString();
        }
    }
}