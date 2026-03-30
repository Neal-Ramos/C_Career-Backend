using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.features.JobApplications.DTOs;

namespace API.helper
{
    public static class ToArrayFiles
    {
        public static FileUploadDTO[] ToFileUploadDTOs(this IFormFileCollection files)
        {
            if (files == null || files.Count == 0)
                return Array.Empty<FileUploadDTO>();

            return files.Select(file => new FileUploadDTO
            {
                FileName = file.FileName,
                Name = file.Name,
                ContentType = file.ContentType,
                Content = file.OpenReadStream()
            }).ToArray();
        }
    }
}