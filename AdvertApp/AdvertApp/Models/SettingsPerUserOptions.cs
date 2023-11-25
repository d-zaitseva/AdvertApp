namespace AdvertApp.Models;

public sealed record class SettingsPerUserOptions
{
    /// <summary>
    /// Max advert amount that 1 user can publish
    /// </summary>
    public int MaxAdvertAmount { get; init; } = 10;
}
