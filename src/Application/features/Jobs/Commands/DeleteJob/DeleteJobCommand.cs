using MediatR;

namespace Application.features.Jobs.Commands.DeleteJob
{
    public class DeleteJobCommand: IRequest<Guid>
    {
        public Guid JobId {get; set;}
    }
}