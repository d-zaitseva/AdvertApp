using Microsoft.AspNetCore.Http;
using AdvertApp.Application.ApplicationServices.Contracts;
using Microsoft.Extensions.Configuration;

namespace AdvertApp.Application.ApplicationServices;

public class ImageApplicationService : IImageApplicationService
{
    private readonly IConfiguration _configuration;

    public ImageApplicationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <inheritdoc />
    public async Task<string> UploadAsync(IFormFile file)
    {
        if (file.Length > 0)
        {
            if (!Directory.Exists(_configuration["StoredFilesPath"]))
            {
                Directory.CreateDirectory(_configuration["StoredFilesPath"]);
            }

            var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_configuration["StoredFilesPath"], fileName);

            using (var stream = File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }
            return filePath;
        }

        return string.Empty;
    }

    /// <inheritdoc />
    public IFormFile GetImageFile(string path)
    {
        IFormFile file;
        if (!string.IsNullOrEmpty(path))
        {
            using (var stream = File.OpenRead(path))
            {
                file = new FormFile(stream, 0, stream.Length, "Image", Path.GetFileName(stream.Name))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg"
                };
            }

            return file;
        }

        return null;
    }

    public void DeleteImageFile(string path)
    {
        if (!string.IsNullOrEmpty(path))
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}