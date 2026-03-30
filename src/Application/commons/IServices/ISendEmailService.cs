using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.commons.DTOs;

namespace Application.commons.IServices
{
    public interface ISendEmailService
    {
        Task<SentEmailDto> SendEmailAsync(
            string To,
            string Subject,
            string HtmlContent
        );
    }
}