using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using SchoolMedicalSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Infrastructure.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        public CloudinaryService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<string> DeleteImageAsync(string publicId)
        {
            var deletionParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deletionParams);

            if (result.Result == "ok")
                return "Xóa ảnh thành công";
            else
                throw new Exception($"Xóa ảnh thất bại: {result.Result}");
        }

        public async Task<string> UploadImageAsync(IFormFile fileImg)
        {
            if (fileImg == null || fileImg.Length == 0)
                throw new ArgumentException("File không hợp lệ");

            using var stream = fileImg.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileImg.FileName, stream),
                //Transformation = new Transformation().Width(500).Height(750).Crop("fill")
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl.AbsoluteUri;
        }
    }
}
