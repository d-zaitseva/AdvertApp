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

    [HttpGet(Name = "GetAdvert")]
    public ActionResult<IEnumerable<AdvertViewModel>> Get(CancellationToken cancellationToken)
    {
        var collection = _advertApplicationService.GetAllAsync(cancellationToken);

        return Ok(collection);
    }

    [HttpPost(Name = "PostAdvert")]
    public async Task<ActionResult<AdvertViewModel>> Post(CreateAdvertFormModel model)
    {
        var response = await _advertApplicationService.AddAsync(model);

        return Ok(response);
    }

    [HttpPut(Name = "PutAdvert")]
    public async Task<ActionResult<AdvertViewModel>> Put(UpdateAdvertFormModel model)
    {
        var response = await _advertApplicationService.UpdateAsync(model);

        return Ok(response);
    }

    [HttpDelete(Name = "DeleteAdvert")]
    public async Task<ActionResult> Delete (DeleteAdvertFormModel model)
    {
        await _advertApplicationService.DeleteAsync(model);

        return Ok();
    }
}
