using KRD.Data.Enum;
using KRD.Data.Models;

namespace KRD.Repo;

public interface IOrdersRepository
{
    Task<Car?> FindCarAsync(string brand, string model, string generation, string config, Color color);
    Task<int> CreateOrderStatusAsync(OrderStatus status);
    Task<int> CreateContactAsync(Contact contact);
    Task<int> CreateOrderAsync(Orders order);
    Task<int> CalculateTotalPriceAsync(int carId);
}