using Application.commons.DTOs;
using Application.commons.IServices;
using Microsoft.Extensions.Configuration;
using Resend;

namespace Infrastructure.Services.ResendServices
{
    public class ResendRepository: ISendEmailService
    {
        private readonly IResend _resend;
        private readonly IConfiguration _configuration;
        public ResendRepository(
            IResend resend,
             IConfiguration configuration
        )
        {
            _resend = resend;
            _configuration = configuration;
        }

        public async Task<SentEmailDto> SendEmailAsync(
            string To,
            string Subject,
            string HtmlContent
        )
        {
            var message = new EmailMessage();

            message.From = _configuration.GetSection("Resend:FromEmail").Value!;
            message.To.Add(To);
            message.Subject = Subject;
            message.HtmlBody = HtmlContent;

            var send = await _resend.EmailSendAsync(message);

            return new SentEmailDto
            {
                IsSuccess = send.Success
            };
        }
    }
}