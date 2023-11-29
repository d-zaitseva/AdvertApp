﻿using AdvertApp.ApplicationServices.Contracts;
using AdvertApp.Models.FormModels;
using AdvertApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdvertApp.Controllers;

[ApiController]
[Route("[controller]")]
public class AdvertController : Controller
{
    private readonly ILogger<AdvertController> _logger; private readonly IAdvertApplicationService _advertApplicationService;

    public AdvertController(ILogger<AdvertController> logger, IAdvertApplicationService advertApplicationService)
    {
        _logger = logger;
        _advertApplicationService = advertApplicationService;
    }

    /// <summary>
    /// Get Collection of all Adverts.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet(Name = "GetAdvert")]
    public async Task<ActionResult<IEnumerable<AdvertViewModel>>> Get(CancellationToken cancellationToken)
    {
        var collection = await _advertApplicationService.GetAllAsync(cancellationToken);

        return Ok(collection);
    }

    /// <summary>
    /// Post new advert.
    /// </summary>
    /// <param name="model">Model to create new advert.</param>
    /// <returns></returns>
    [HttpPost(Name = "PostAdvert")]
    public async Task<IActionResult> Post([FromForm]CreateAdvertFormModel model)
    {
        var response = await _advertApplicationService.AddAsync(model);

        if (response.IsSuccess)
        {
            return Ok();
        }

        return BadRequest(response.Error);
    }

    /// <summary>
    /// Update existing advert.
    /// </summary>
    /// <param name="model">Model to update advert.</param>
    /// <returns></returns>
    [HttpPut(Name = "PutAdvert")]
    public async Task<IActionResult> Put([FromForm]UpdateAdvertFormModel model)
    {
        var response = await _advertApplicationService.Update(model);

        if (response.IsFailure)
        {
            BadRequest(response.Error);
        }

        return Ok();
    }

    /// <summary>
    /// Delete Advert - change status of the advert into Deleted.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpDelete(Name = "DeleteAdvert")]
    public async Task<ActionResult> Delete ([FromBody] DeleteAdvertFormModel model)
    {
        var response = await _advertApplicationService.DeleteAsync(model);

        if (response.IsFailure)
        {
            BadRequest(response.Error);
        }

        return Ok();
    }
}
