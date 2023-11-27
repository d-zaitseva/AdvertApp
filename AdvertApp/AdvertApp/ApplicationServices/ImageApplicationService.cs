using AdvertApp.ApplicationServices.Contracts;
using AdvertApp.EF.Entities;

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
        image.Name = formFile.FileName;
        image.Data = bytes;

        return image;
    }

    /// <inheritdoc />
    public IFormFile ConvertImageToFormFile(Image image)
    {
        return null;
    }
}
