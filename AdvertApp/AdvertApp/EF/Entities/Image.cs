namespace AdvertApp.EF.Entities;

public class Image
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public byte[] Data { get; set; } = new byte[0];
    public Advert Advert { get; set; } = null!;
}
