
using System.Threading.Tasks;
using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.Interfaces;
using Aerolineas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aerolineas.Controllers;

[ApiController]
[Route("/api/vuelos")]
public class VueloController(IVuelosService vuelosService) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Task<List<Vuelo>>>> Get()
    {
        var vuelos = await vuelosService.ConsultarVuelos();
        return Ok(vuelos);
    }

    [HttpGet("/{id}")]
    [Authorize]
    public async Task<ActionResult<Vuelo>> GetById(int id)
    {
        Vuelo? vuelo = await vuelosService.ConsultarVuelo(id);
        if (vuelo == null) return NotFound();
        return Ok(vuelo);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<Vuelo>> Create([FromBody] VueloDTO vueloDTO)
    {
        Vuelo vuelo = await vuelosService.RegistrarVuelo(vueloDTO);
        return Ok(vuelo);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<Vuelo>> Update([FromBody] VueloDTO vuelo, int id)
    {
        var response = await vuelosService.ModificarVuelo(id, vuelo);
        return response.ToActionResult();
    }

    [HttpDelete("{id}/cancelar")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<Vuelo>> Cancel([FromBody] Vuelo vuelo, int id)
    {
        var response = await vuelosService.CancelarVuelo(id);
        return response.ToActionResult();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<bool>> Eliminar([FromBody] Vuelo vuelo, int id)
    {
        var response = await vuelosService.EliminarVuelo(id);
        return response.ToActionResult();
    }

    [HttpPost("/{id}/asignar_slot")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<Vuelo>> AsignarSlot(int id)
    {
        Result<int> slotResponse = await vuelosService.BuscarSlot();
        if (slotResponse.Success)
        {
            var slot = await vuelosService.AsignarSlot(id, slotResponse.Value);
            return slot.ToActionResult();
        }


        return BadRequest(new { error = "No hay slot" });
    }

    [HttpPost("/{id}/confirmar")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<Vuelo>> ConfirmarVuelo(int id)
    {
        var response = await vuelosService.ConfirmarVuelo(id);
        return response.ToActionResult();
    }
}