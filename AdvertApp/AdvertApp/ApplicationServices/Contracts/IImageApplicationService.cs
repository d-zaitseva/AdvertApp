using AdvertApp.EF.Entities;

namespace AdvertApp.ApplicationServices.Contracts;

public interface IImageApplicationService
{
    /// <summary>
    /// Allows to convert IFormFile to Image entity.
    /// </summary>
    /// <param name="formFile">IFormFile object</param>
    /// <returns></returns>
    Image ConvertFormFileToImage (IFormFile formFile);

    /// <summary>
    /// Allows to convert Image entity to IFormFile.
    /// </summary>
    /// <param name="image">Image</param>
    /// <returns></returns>
    IFormFile ConvertImageToFormFile(Image image);
}
