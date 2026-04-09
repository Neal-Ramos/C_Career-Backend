using Application.commons.DTOs;
using Application.features.Applications.DTOs;
using MediatR;

namespace Application.features.Applications.Commands.AddApplication
{
    public class AddApplicationCommand: IRequest<ApplicationDto>
    {
        public string FirstName {get; set;} = null!;
        public string MiddleName {get; set;} = null!;
        public string LastName {get; set;} = null!;
        public string Email {get; set;} = null!;
        public string ContactNumber {get; set;} = null!;
        public string UniversityName {get; set;} = null!;
        public string Degree {get; set;} = null!;
        public string CustomFields {get; set;} = null!;
        public int GraduationYear {get; set;}
        public List<FileUploadDTO> SubmittedFile {get; set;} = [];
        public Guid JobId {get; set;}
    }
}