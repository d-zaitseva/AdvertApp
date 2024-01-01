using AdvertApp.Contracts.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace AdvertApp.Contracts.Models;

public class FilterRequest
{
    public const int DefaultPage = 1;
    public const int DefaultPageSize = 100;
    public const int MaxPageSize = 200;
    public const int DefaultSortField = 0;

    /// <summary>
    /// Page number.
    /// If not specified use DefaultPage value.
    /// </summary>
    [Range(DefaultPage, int.MaxValue)]
    public int Page { get; set; } = DefaultPage;

    /// <summary>
    /// Amount of items per page.
    /// If not specified use DefaultPageSize value
    /// </summary>
    [Range(1, MaxPageSize)]
    public int PageSize { get; set; } = DefaultPageSize;

    /// <summary>
    /// Sort parameter.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public SortByFieldsForAdvert SortBy { get; set; } = DefaultSortField;

    /// <summary>
    /// Sort direction
    /// </summary>
    public bool SortAsc { get; set; } = false;

    /// <summary>
    /// Searched advert status
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public AdvertStatus? Status { get; set; } = null;

    /// <summary>
    /// Date advert was created
    /// </summary>
    public DateTime? CreatedAt { get; set; } = null;

    /// <summary>
    /// Date advert was updated
    /// </summary>
    public DateTime? UpdatedAt { get; set; } = null;

    /// <summary>
    /// Min rating of adverts
    /// </summary>
    public int? MinRating { get; set; } = null;

    /// <summary>
    /// Max rating of adverts
    /// </summary>
    public int? MaxRating { get; set; } = null;

    /// <summary>
    /// Searched text at author name and advert text
    /// </summary>
    public string FullTextSearch { get; set; } = string.Empty;
}