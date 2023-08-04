using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
       public EmailSettings _emailSettings { get; }
        public ILogger<EmailService> _logger { get; }

        public EmailService(IOptions<EmailSettings> emailSettings , ILogger<EmailService> logger) //estamos enviando un diccionario de datos en el email settings por eso cambiamos a ioptions
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task<bool> SendMail(Application.Models.Email email)
        {
            var cliente = new SendGridClient(_emailSettings.ApiKey);
            var subject = email.Body;
            var to = new EmailAddress(email.To);


            var emailBody = email.Body;

            var from = new EmailAddress
            {
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FromName
            };

            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);//contieod palno y contneigo html en bmbos casoe s igual
            var response = await cliente.SendEmailAsync(sendGridMessage); 

            if(response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            _logger.LogError("El email no pudo enviarse, existen errores");
            return false;



        }
    }
}
