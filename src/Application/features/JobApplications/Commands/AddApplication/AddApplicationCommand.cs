using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.features.JobApplications.DTOs;
using MediatR;

namespace Application.features.JobApplications.Commands.AddApplication
{
    public class AddApplicationCommand: IRequest<JobApplicationDto>
    {
        public string FirstName {get; set;} = string.Empty;
        public string MiddleName {get; set;} = string.Empty;
        public string LastName {get; set;} = string.Empty;
        public string Email {get; set;} = string.Empty;
        public string ContactNumber {get; set;} = string.Empty;
        public string UniversityName {get; set;} = string.Empty;
        public string Degree {get; set;} = string.Empty;
        public int GraduationYear {get; set;}
        public List<FileUploadDTO> SubmittedFile {get; set;} = [];
        public Guid JobId {get; set;}
    }
}