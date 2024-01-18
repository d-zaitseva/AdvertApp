using Microsoft.AspNetCore.Mvc;
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
    /// <response code="200">When added successfully</response>
    /// <response code="400">When an error occurred</response>
    [HttpPost(Name = "PostAdvert")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    /// <response code="200">When updated successfully</response>
    /// <response code="400">When an error occurred</response>
    [HttpPut(Name = "PutAdvert")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    /// <response code="200" > When deleted successfully</response>
    /// <response code="400">When an error occurred</response>
    [Route("/softdeleteadvert")]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SoftDelete([FromQuery] DeleteAdvertFormModel model)
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
    /// <response code="200" > When deleted successfully</response>
    /// <response code="400">When an error occurred</response>
    [HttpDelete(Name = "DeleteAdvert")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete([FromQuery] DeleteAdvertFormModel model)
    {
        var response = await _advertApplicationService.DeleteAsync(model);

        if (response.IsFailure)
        {
            return BadRequest(response.Error);
        }

        return Ok();
    }
}
