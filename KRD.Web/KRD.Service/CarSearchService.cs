using KRD.Data.Models;
using KRD.Repo;
using Microsoft.EntityFrameworkCore;

namespace KRD.Service;

public class CarSearchService:ICarSearchService
{
    private readonly AppDbContext _db;

    public CarSearchService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<string>> GetAllBrandsAsync()
    {
        return await _db.CarsDb
            .Select(c => c.Brand)
            .Distinct()
            .OrderBy(b => b)
            .ToListAsync();
    }

    public async Task<List<string>> GetModelsByBrandAsync(string brand)
    {
        return await _db.CarsDb
            .Where(c => c.Brand == brand)
            .Select(c => c.Model)
            .Distinct()
            .OrderBy(m => m)
            .ToListAsync();
    }

    public async Task<List<string>> GetGenerationsByModelAsync(string brand, string model)
    {
        return await _db.CarsDb
            .Where(c => c.Brand == brand && c.Model == model)
            .Select(c => c.Generation)
            .Distinct()
            .OrderBy(g => g)
            .ToListAsync();
    }

    public async Task<List<string>> GetConfigsByGenerationsAsync(string brand, string model, string generation)
    {
        return await _db.CarsDb
            .Where(c => c.Brand == brand && c.Model == model && c.Generation == generation)
            .Select(c => c.Config)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();
    }

    public async Task<List<Option>> GetOptionsByConfig(string brand, string model, string generation, string config)
    {
        var car= await _db.CarsDb
            .FirstOrDefaultAsync(c=>c.Brand == brand && c.Model == model && c.Config == config);
        if (car == null)
            return new List<Option>();
        var optionId= await _db.CarOptionsDb
            .Where(c => c.CarId == car.Id)
            .Select(c => c.OptionId)
            .ToListAsync();
        return await _db.OptionsDb
            .Where(o=>optionId.Contains(o.Id))
            .ToListAsync();
    }
}