using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.features.Jobs.DTOs;
using MediatR;

namespace Application.features.Jobs.Queries.GetJobsById
{
    public class GetJobsByIdQuery: IRequest<JobsDto?>
    {
        public Guid JobId {get; set;}
    }
}