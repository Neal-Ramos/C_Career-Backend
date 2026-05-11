using Application.commons.DTOs;
using Application.commons.IServices;
using Google.GenAI;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services.AIService
{
    public class GeminiService
    {
        private readonly Client _client;
        public GeminiService(
            IConfiguration configuration
        )
        {
            _client = new Client(apiKey: configuration["GoogleApiKey"]);
        }
        public async Task<ApplicationCheckDto> AnalyzeApplicationAsync(
            JobDto Job,
            ApplicationDto Application
        )
        {
            var prompt = $@"
                Compare this job application to the job requirements.
                Return ONLY raw JSON, no markdown, no explanation.

                JSON format:
                {{
                    ""score"": 0-100,
                    ""verdict"": ""Pass"" or ""Fail"",
                }}

                Job Requirements: {Job}
                Application: {Application}
            ";


            var res = await _client.Models.GenerateContentAsync(
                model: "gemini-2.0-flash-lite", contents: prompt
            );

            return new ApplicationCheckDto
            {
                Verdict = res.ToString()
            };
        }
    }
}