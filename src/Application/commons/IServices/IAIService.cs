
using Application.commons.DTOs;

namespace Application.commons.IServices
{
    public interface IAIService
    {
        Task<ApplicationCheckDto> AnalyzeApplicationAsync(
            JobDto Job,
            ApplicationDto Application
        );
    }
}