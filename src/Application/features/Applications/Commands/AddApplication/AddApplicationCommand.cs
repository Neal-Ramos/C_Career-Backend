using Application.commons.DTOs;
using Application.features.Applications.DTOs;
using MediatR;

namespace Application.features.Applications.Commands.AddApplication
{
    public class AddApplicationCommand: IRequest<ApplicationDto>
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