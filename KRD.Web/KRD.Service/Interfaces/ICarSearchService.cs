using KRD.Data.Models;

namespace KRD.Service;

public interface ICarSearchService
{
    Task<List<string>> GetAllBrandsAsync();
    Task<List<string>> GetModelsByBrandAsync(string brand); 
    Task<List<string>> GetGenerationsByModelAsync(string brand, string model);
    Task<List<string>> GetConfigsByGenerationsAsync(string brand, string model, string generation);
    Task<List<Option>> GetOptionsByConfig(string brand, string model, string generation, string config);
}