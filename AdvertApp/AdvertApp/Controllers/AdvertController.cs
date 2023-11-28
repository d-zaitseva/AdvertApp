using AdvertApp.ApplicationServices.Contracts;
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
    /// Get Collection of Adverts.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet(Name = "GetAdvert")]
    public ActionResult<IEnumerable<AdvertViewModel>> Get(CancellationToken cancellationToken)
    {
        var collection = _advertApplicationService.GetAllAsync(cancellationToken);

        return Ok(collection);
    }

    [HttpPost(Name = "PostAdvert")]
    public async Task<IActionResult> Post(CreateAdvertFormModel model)
    {
        var response = await _advertApplicationService.AddAsync(model);

        if (response.IsSuccess)
        {
            return Ok();
        }

        return BadRequest(response.Error);
    }

    [HttpPut(Name = "PutAdvert")]
    public async Task<IActionResult> Put(UpdateAdvertFormModel model)
    {
        var response = await _advertApplicationService.Update(model);

        if (response.IsFailure)
        {
            BadRequest(response.Error);
        }

        return Ok();
    }

    [HttpDelete(Name = "DeleteAdvert")]
    public async Task<ActionResult> Delete (DeleteAdvertFormModel model)
    {
        var response = await _advertApplicationService.DeleteAsync(model);

        if (response.IsFailure)
        {
            BadRequest(response.Error);
        }

        return Ok();
    }
}
