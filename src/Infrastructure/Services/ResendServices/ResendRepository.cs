using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Application.commons.DTOs;
using Application.commons.IServices;
using Microsoft.Extensions.Configuration;
using Resend;

namespace Infrastructure.Services.ResendServices
{
    public class ResendRepository: ISendEmailService
    {
        private readonly IResend _resend;
        public ResendRepository(IResend resend)
        {
            _resend = resend;
        }

        public async Task<SentEmailDto> SendEmailAsync(
            string To,
            string Subject,
            string HtmlContent
        )
        {
            var message = new EmailMessage();

            message.From = "service@resend.dev";
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