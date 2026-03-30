using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Application.commons.DTOs;
using Application.commons.IServices;

namespace Infrastructure.Services.CodeGenerator
{
    public class CodeGeneratorRepository: IGenerateCodeService
    {
        public Task<GenerateCodeDto> Generate()
        {
            var bytes = RandomNumberGenerator.GetBytes(6);
            var code = string.Concat(bytes.Select(b => (b % 10).ToString()));
            var expiry = DateTime.UtcNow.AddMinutes(2);

            var result = new GenerateCodeDto
            {
                Code = code,
                DateExpiry = expiry
            };

            return Task.FromResult(result);
        }
    }
}