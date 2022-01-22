using Microsoft.AspNetCore.Http;

namespace Blog.Services
{
    public interface IFileUploadService
    {
        Task<string> Upload(IFormFile file);
        void Delete(string path);
    }
}
