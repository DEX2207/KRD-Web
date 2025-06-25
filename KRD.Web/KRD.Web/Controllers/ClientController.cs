using KRD.Data.Models;
using KRD.Repo;
using KRD.Service;
using Microsoft.AspNetCore.Mvc;

namespace KRD.Web.Controllers;

public class ClientController:ControllerBase
{
    private readonly IRepository<Client> _clientRepository;

    public ClientController(IRepository<Client> clientRepository)
    {
        _clientRepository = clientRepository;
    }

    [HttpGet("Clients")]
    public async Task<IActionResult> Clients()
    {
        return Ok(await _clientRepository.GetAllAsync());
    }
}