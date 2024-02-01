using Microsoft.AspNetCore.Http;
using Reactivities.Application.Photos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactivities.Application.Interfaces
{
    public interface IPhotoAccessor
    {
        Task<PhotoUploadResult> AddPhoto(IFormFile file);

        Task<string> DeletePhoto(string publicId);
    }
}
