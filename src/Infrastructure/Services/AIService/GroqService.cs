using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Application.commons.DTOs;
using Application.commons.IServices;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services.AIService
{
    public class GroqService: IAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _aIModel;
        private readonly string _apiKey;
        private readonly string _url;
        public GroqService(
            HttpClient httpClient,
            IConfiguration configuration
        )
        {
            _httpClient = httpClient;
            _url = configuration["Groq:Url"]!;
            _apiKey = configuration["Groq:ApiKey"]!;
            _aIModel = configuration["Groq:AiModel"]!;
        }
        public async Task<ApplicationCheckDto> AnalyzeApplicationAsync(
            JobDto Job,
            ApplicationDto Application
        )
        {
            var prompt = $@"
                Compare this job application to the job requirements.
                Parse the Stringified fields from application and jobs and check if they answerd according to the questions context
                Also check their input if its appropriate for applying jobs typos are okay but not words that are not unrelated to the job
                And then give a Reason why you gave that Response using 1 to 4 sentences and Also Interview Suggestion using 1 to 4 sentences
                Return ONLY raw JSON, no markdown, no explanation.

                JSON format:
                {{
                    ""Score"": 0-100,
                    ""Verdict"": ""Pass"" or ""Fail"",
                    ""Reason"": ""Give specific reason why you gave that verdict using 1-4 sentences"",
                    ""InterviewSuggestion"": ""Give a Interview Suggestions to focus on""
                }}

                Job Requirements: {Job}
                Application: {Application}
            ";
            var requestBody = new
            {
                model = _aIModel,
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, _url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            request.Content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json"
            );
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<JsonElement>();
            var rawText = json
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            var cleaned = rawText!
                .Replace("\n", "")
                .Replace("```json", "")
                .Replace("```", "")
                .Trim();

            return JsonSerializer.Deserialize<ApplicationCheckDto>(cleaned)!;
        }
    }
}