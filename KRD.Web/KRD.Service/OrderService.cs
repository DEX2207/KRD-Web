using KRD.Data.Enum;
using KRD.Data.Models;
using KRD.Repo;
using KRD.Service.Vallidators;

namespace KRD.Service;

public class OrderService:IOrderService
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly AppDbContext _context;
    private readonly ContactValidator  _contactValidator;
    private readonly OrderValidator  _orderValidator;

    public OrderService(IOrdersRepository ordersRepository, AppDbContext context,  ContactValidator contactValidator, OrderValidator orderValidator)
    {
        _ordersRepository=ordersRepository;
        _context = context;
        _contactValidator = contactValidator;
        _orderValidator = orderValidator;
    }

    public async Task CreateOrderAsync(string fullname, string address, string brand, string model, string generation,
        string config, Color color, string email, string phonenumber)
    {
        var car=await _ordersRepository.FindCarAsync(brand, model, generation, config, color);
        if(car==null) throw new Exception("Машина не найдена");
        var contact = new Contact
        {
            PhoneNumber = phonenumber,
            Email = email
        };
        var validatorResult= await _contactValidator.ValidateAsync(contact);
        if(!validatorResult.IsValid)
            throw new Exception("Номер телефона или почта не валидны");
        var orderStatus = new OrderStatus
        {
            Status = Status.received,
            Date = DateTime.UtcNow
        };
        int price= await _ordersRepository.CalculateTotalPriceAsync(car.Id);

        var order = new Orders()
        {
            BuyerFullName = fullname,
            BuyerAddress = address,
            CarId = car.Id,
            PaymentInvoce = price
        };
        
        var orderValidator= await _orderValidator.ValidateAsync(order);
        if(!orderValidator.IsValid) throw new Exception("ФИО или адрес отсутствуют или не валидны");
        await _ordersRepository.CreateContactAsync(contact);
        await _ordersRepository.CreateOrderStatusAsync(orderStatus);
        order.ContactId=contact.Id;
        order.OrderStatusId=orderStatus.Id;
        await _ordersRepository.CreateOrderAsync(order);
    }

    public async Task ChangeStatus(int orderId, Status status)
    {
        var order = await _context.OrdersDb.FindAsync(orderId);
        if (order==null) throw new Exception("Заказ не найден");
        var orderStatus = await _context.OrderStatusDb.FindAsync(order.OrderStatusId);
        if (orderStatus==null) throw new Exception("Статус заказа не найден");
        orderStatus.Status = status;
        orderStatus.Date = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }
}