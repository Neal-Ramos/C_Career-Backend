
using Domain.enums;

namespace API.contracts.Application
{
    public class PatchApplicationStatusReq
    {
        public ApplicationStatusEnum Status {get; set;}
    }
}