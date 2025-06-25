using KRD.Repo;
using KRD.Service;
using Microsoft.AspNetCore.Mvc;

namespace KRD.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarSearchController:ControllerBase
{
    /*private readonly ILogger<CarSearchController> _logger;
    public CarSearchController(ILogger<CarSearchController> logger)*/
    
    private readonly ICarSearchService _carSearchService;
    public CarSearchController(AppDbContext context, ICarSearchService carSearchService)
    {
        _carSearchService = carSearchService;
    }

    [HttpGet("brands")]
    public async Task<IActionResult> GetBrands()
    {
        var brands = await _carSearchService.GetAllBrandsAsync();
        if (brands != null)
        {
            return Ok(brands);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("models")]
    public async Task<IActionResult> GetModels([FromQuery] string brand)
    {
        var models = await _carSearchService.GetModelsByBrandAsync(brand);
        return Ok(models);
    }

    [HttpGet("generations")]
    public async Task<IActionResult> GetGenerations([FromQuery] string brand, [FromQuery] string model)
    {
        var generations = await _carSearchService.GetGenerationsByModelAsync(brand, model);
        return Ok(generations);
    }
    
    [HttpGet("configs")]
    public async Task<IActionResult> GetConfigs([FromQuery] string brand, [FromQuery] string model, [FromQuery] string generation)
    {
        var configs = await _carSearchService.GetConfigsByGenerationsAsync(brand, model, generation);
        return Ok(configs);
    }
    
    [HttpGet("options")]
    public async Task<IActionResult> GetOptions([FromQuery] string brand, [FromQuery] string model, [FromQuery] string generation, [FromQuery] string option)
    {
        var options = await _carSearchService.GetOptionsByConfig(brand, model, generation, option);
        return Ok(options);
    }
}