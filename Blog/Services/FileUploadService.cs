namespace Blog.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment env;

        public FileUploadService(IWebHostEnvironment _env)
        {
            env = _env;
        }

        public void Delete(string path)
        {
            File.Delete(Path.Combine(env.WebRootPath, path));
        }


        public async Task<string> Upload(IFormFile file)
        {
            if (file != null && file.Length != 0)
            {
                string pathforDb = "";

                try
                {
                    pathforDb = Path.Combine("upload/", Guid.NewGuid().ToString()
                            + file.FileName.Substring(file.FileName.LastIndexOf('.')));
                    var imageFullName = Path.Combine(env.WebRootPath, pathforDb);
                    if (file.Length > 0)
                    {
                        using (var stream = new FileStream(imageFullName, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                }
                catch (Exception)
                {
                    //todo: loglama işlemi yapabilirsin.
                }
                return pathforDb;
            }
            return null;

        }
    }
}
