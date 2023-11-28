namespace AdvertApp.EF.Entities;

public class Image
{
    /// <summary>
    /// Image Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// File Name.
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// File type.
    /// </summary>
    public string Type {  get; set; } = string.Empty;

    /// <summary>
    /// ContentDisposition
    /// </summary>
    public string ContentDisposition {  get; set; } = string.Empty;

    /// <summary>
    /// Image Data.
    /// </summary>
    public byte[] Data { get; set; } = new byte[0];

    public Advert Advert { get; set; } = null!;
}
