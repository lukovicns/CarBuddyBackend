using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CarBuddy.WebApi.Services
{
    public class UploadedFile
    {
        private readonly string _path;

        public UploadedFile(string path)
        {
            _path = path;
        }

        public async Task<string> Upload(IFormFile file)
        {
            var fileName = GenerateFileName(file.FileName);

            await using (var stream = File.Create(Path.Combine(_path, fileName)))
            {
                await file.CopyToAsync(stream);
            }
            
            return "/Images/" + fileName;
        }

        private string GenerateFileName(string fileName) => $"{Guid.NewGuid()}-{fileName}";
    }
}
