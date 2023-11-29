using AdvertApp.ApplicationServices.Contracts;
using AdvertApp.EF.Entities;
using System.Collections;
using System.IO;
using System.Net.Mime;

namespace AdvertApp.ApplicationServices;

public class ImageApplicationService : IImageApplicationService
{
    /// <inheritdoc />
    public Image ConvertFormFileToImage(IFormFile formFile)
    {
        Image image = new Image();
        // TO DO --- Add try - catch
        long fileSize = formFile.Length;
        string fileType = formFile.ContentType;
        byte[] bytes = new byte[fileSize];

        if (fileSize > 0)
        {
            using (var stream = new MemoryStream())
            {
                formFile.CopyTo(stream);
                bytes = stream.ToArray();
            }
        }

        image.Id = Guid.NewGuid();
        image.FileName = formFile.FileName;
        image.Name = formFile.Name;
        image.Type = fileType;
        image.ContentDisposition = formFile.ContentDisposition.ToString();
        image.Data = bytes;

        return image;
    }

    /// <inheritdoc />
    public IFormFile ConvertImageToFormFile(Image image)
    {
        IFormFile file;
        using (var stream = new MemoryStream(image.Data))
        {
            file = new FormFile(stream, 0, image.Data.Length, image.Name, image.FileName)
            {
                Headers = new HeaderDictionary(),
                ContentDisposition = image.ContentDisposition,
                ContentType = image.Type
            };
        }

        return file;
    }
}