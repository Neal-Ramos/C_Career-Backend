using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.commons.DTOs;

namespace Application.commons.IServices
{
    public interface IGenerateCodeService
    {
        Task<GenerateCodeDto> Generate();
    }
}