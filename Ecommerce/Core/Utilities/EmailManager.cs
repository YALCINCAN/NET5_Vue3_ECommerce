﻿using Core.Configurations;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities
{
    public class EmailManager : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        public EmailManager(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }
        public async Task ConfirmationMailAsync(string link, string email)
        {
            var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
            {
                Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password),
                EnableSsl = _emailSettings.EnableSSL
            };
            var subject = $"www.eCommerce.com || Confirm Email";
            var body = "<h2>Please click this link for confirm email</h2><hr/>";
            body += $"<a href='{link}'>Confirmation Link</a>";
            await client.SendMailAsync(
            new MailMessage(_emailSettings.Email, email, subject, body) { IsBodyHtml = true }
            );
        }

        public async Task ForgetPasswordMailAsync(string link, string email)
        {
            var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
            {
                Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password),
                EnableSsl = _emailSettings.EnableSSL
            };
            var subject = $"www.eCommerce.com || Reset Password";
            var body = "<h2>Please click this link for reset password</h2><hr/>";
            body += $"<a href='{link}'>reset password link</a>";
            await client.SendMailAsync(
            new MailMessage(_emailSettings.Email, email, subject, body) { IsBodyHtml = true }
            );
        }
    }
}
