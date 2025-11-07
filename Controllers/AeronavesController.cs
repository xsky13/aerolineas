using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.Interfaces;
using Aerolineas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aerolineas.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AeronavesController : ControllerBase
{
    private readonly IAeronaveService _aeronaveService;

    public AeronavesController(IAeronaveService aeronaveService)
    {
        _aeronaveService = aeronaveService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Aeronave>>> GetAll()
    {
        var result = await _aeronaveService.GetAllAeronaves();
        return result.ToActionResult();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Aeronave>> GetById(int id)
    {
        var result = await _aeronaveService.GetAeronaveById(id);
        return result.ToActionResult();
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Aeronave>> Create(CreateAeronaveDTO dto)
    {
        var result = await _aeronaveService.CreateAeronave(dto);
        return result.ToActionResult();
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<Aeronave>> Update(int id, UpdateAeronaveDTO dto)
    {
        var result = await _aeronaveService.UpdateAeronave(id, dto);
        return result.ToActionResult();
    }


    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var result = await _aeronaveService.DeleteAeronave(id);
        return result.ToActionResult();
    }
}