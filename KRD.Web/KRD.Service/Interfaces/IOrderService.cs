using KRD.Data.Enum;

namespace KRD.Service;

public interface IOrderService
{
    Task CreateOrderAsync(string fullname, string address, string brand, string model, string generation, string config, Color color, string email, string phonenumber);
    Task ChangeStatus(int orderId, Status status);
}