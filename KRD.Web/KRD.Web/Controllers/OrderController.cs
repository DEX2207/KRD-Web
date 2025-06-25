using KRD.Data.Enum;
using KRD.Data.Models;
using KRD.Repo;
using KRD.Service;
using Microsoft.AspNetCore.Mvc;

namespace KRD.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController:ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IRepository<Orders> _repository;
    public OrderController(IOrderService orderService, IRepository<Orders> repository)
    {
        _orderService = orderService;
        _repository = repository;
    }

    [HttpPost("CreateOrder")]
    public async Task<IActionResult> CreateOrder([FromQuery] string fullname, [FromQuery] string address, [FromQuery] string brand, [FromQuery] string model, [FromQuery] string generation,
        [FromQuery] string config, [FromQuery] Color color, [FromQuery] string email, [FromQuery] string phonenumber)
    {
        await _orderService.CreateOrderAsync(fullname, address, brand, model, generation, config, color, email, phonenumber);
        return Ok("Заказ создан!");
    }

    [HttpPost("ChangeStatusOrder")]
    public async Task<IActionResult> ChangeStatus([FromQuery] int orderId, [FromQuery] Status status )
    {
        await _orderService.ChangeStatus(orderId, status);
        return Ok("Статус заказа обновлен!");
    }

    [HttpGet("GetOrders")]
    public async Task<IActionResult> GetOrders()
    {
        return Ok(await _repository.GetAllAsync());
    }
}