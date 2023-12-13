using Microsoft.AspNetCore.Http;
using AdvertApp.Domain.Entities;

namespace AdvertApp.Application.ApplicationServices.Contracts;

public interface IImageApplicationService
{
    /// <summary>
    /// Uploads file to disc.
    /// </summary>
    /// <param name="file">File to upload.</param>
    /// <returns>Path of saved file</returns>
    Task<string> UploadAsync(IFormFile file);

    /// <summary>
    /// Returns file by path.
    /// </summary>
    /// <param name="path">Path of the file.</param>
    /// <returns></returns>
    IFormFile GetImageFile(string path);

    /// <summary>
    /// Deletes file if exists at provided path.
    /// </summary>
    /// <param name="path">Path of the file.</param>
    void DeleteImageFile(string path);
}
