
using System.ComponentModel.DataAnnotations;
using Domain.enums;

namespace API.contracts.Application
{
    public class PatchApplicationStatusReq
    {
        [Required]
        public ApplicationStatusEnum Status {get; set;}
        public DateTime? DateInterview {get; set;}
    }
}