using KRD.Data.Enum;
using KRD.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KRD.Repo;

public class OrdersRepository:IOrdersRepository
{
    private readonly AppDbContext _db;

    public OrdersRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Car?> FindCarAsync(string brand, string model, string generation, string config, Color color)
    {
        return await _db.CarsDb
            .FirstOrDefaultAsync(c =>
                c.Brand == brand &&
                c.Model == model &&
                c.Generation == generation &&
                c.Config == config &&
                c.Color == color);
    }

    public async Task<int> CreateOrderStatusAsync(OrderStatus status)
    {
        _db.OrderStatusDb.Add(status);
        await _db.SaveChangesAsync();
        return status.Id;
    }

    public async Task<int> CreateContactAsync(Contact contact)
    {
        _db.ContactsDb.Add(contact);
        await _db.SaveChangesAsync();
        return contact.Id;
    }
    public async Task<int> CreateOrderAsync(Orders order)
    {
        _db.OrdersDb.Add(order);
        await _db.SaveChangesAsync();
        return order.Id;
    }

    public async Task<int> CalculateTotalPriceAsync(int carId)
    {
        var car = await _db.CarsDb
            .Include(c => c.CarOptions)
            .ThenInclude(co => co.Option)
            .FirstOrDefaultAsync(c => c.Id == carId);

        if (car == null) return 0;

        var optionsPrice = car.CarOptions
            .Where(co => co.Option?.OptionStatus == true)
            .Sum(co => co.Option.OptionPrice);

        return car.BasePrice + optionsPrice;
    }
}