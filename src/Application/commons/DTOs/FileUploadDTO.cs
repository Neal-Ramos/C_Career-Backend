using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.features.JobApplications.DTOs
{
    public class FileUploadDTO
    {
        public string FileName { get; set; } = null!;
        public string Name {get; set;} = null!;
        public string ContentType { get; set; } = null!;
        public Stream Content { get; set; } = null!;

    }
}