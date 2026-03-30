using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.commons.IServices
{
    public class UploadAsyncDto
    {
        public string PublicId {get; set;} = null!;
        public string Path {get; set;} = null!;
    }
}