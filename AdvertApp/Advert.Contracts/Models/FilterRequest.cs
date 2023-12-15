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
}
