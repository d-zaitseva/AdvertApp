﻿using Microsoft.AspNetCore.Mvc;
using AdvertApp.Contracts.Models.FormModels;
using AdvertApp.Contracts.Models.ViewModels;
using AdvertApp.Application.ApplicationServices.Contracts;
using AdvertApp.Contracts.Models;

namespace AdvertApp.Controllers;

[ApiController]
[Route("[controller]")]
public class AdvertController : Controller
{
    private readonly IAdvertApplicationService _advertApplicationService;

    public AdvertController(IAdvertApplicationService advertApplicationService)
    {
        _advertApplicationService = advertApplicationService;
    }

    /// <summary>
    /// Get Collection of all Adverts.
    /// </summary>
    /// <param name="filterRequest">Display options as page number and size, sorting parameters.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AdvertViewModel>>> Get(
        [FromQuery] FilterRequest filterRequest, 
        CancellationToken cancellationToken)
    {
        var collection = await _advertApplicationService.GetAllAsync(filterRequest, cancellationToken);

        return Ok(collection);
    }

    /// <summary>
    /// Post new advert.
    /// </summary>
    /// <param name="model">Model to create new advert.</param>
    /// <returns></returns>
    [HttpPost(Name = "PostAdvert")]
    public async Task<IActionResult> Post([FromForm] CreateAdvertFormModel model)
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
            return BadRequest(response.Error);
        }

        return Ok();
    }

    /// <summary>
    /// Delete Advert - change status of the advert into Deleted.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [Route("/softdeleteadvert")]
    [HttpDelete]
    public async Task<ActionResult> SoftDelete([FromBody] DeleteAdvertFormModel model)
    {
        var response = await _advertApplicationService.SoftDeleteAsync(model);

        if (response.IsFailure)
        {
            return BadRequest(response.Error);
        }

        return Ok();
    }

    /// <summary>
    /// Delete Advert - delete completely from DB.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpDelete(Name = "DeleteAdvert")]
    public async Task<ActionResult> Delete([FromBody] DeleteAdvertFormModel model)
    {
        var response = await _advertApplicationService.DeleteAsync(model);

        if (response.IsFailure)
        {
            return BadRequest(response.Error);
        }

        return Ok();
    }
}
