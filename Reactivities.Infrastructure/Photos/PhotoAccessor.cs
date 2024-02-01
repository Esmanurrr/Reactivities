using Microsoft.AspNetCore.Http;
using Reactivities.Application.Interfaces;
using Reactivities.Application.Photos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reactivities.Infrastructure.Photos
{
    public class PhotoAccessor : IPhotoAccessor
    {
        public Task<PhotoUploadResult> AddPhoto(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeletePhoto(string publicId)
        {
            throw new NotImplementedException();
        }
    }
}
