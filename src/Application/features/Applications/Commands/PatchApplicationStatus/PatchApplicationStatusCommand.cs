
using Application.commons.DTOs;
using Domain.enums;
using MediatR;

namespace Application.features.Applications.Commands.PatchApplicationStatus
{
    public class PatchApplicationStatusCommand: IRequest<ApplicationDto>
    {
        public Guid ApplicationId {get; set;}
        public Guid AdminId {get; set;}
        public ApplicationStatusEnum Status {get; set;}
    }
}