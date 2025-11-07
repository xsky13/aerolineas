
using System.Threading.Tasks;
using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.Interfaces;
using Aerolineas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aerolineas.Controllers;

[ApiController]
[Route("api/vuelos")]
public class VueloController(IVuelosService vuelosService) : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<Vuelo>>> Get()
    {
        var vuelos = await vuelosService.ConsultarVuelos();
        return Ok(vuelos);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<Vuelo>> GetById(int id)
    {
        Vuelo? vuelo = await vuelosService.ConsultarVuelo(id);
        if (vuelo == null) return NotFound();
        return Ok(vuelo);
    }

    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<ActionResult<Vuelo>> Create([FromBody] CrearVueloDTO vueloDTO)
    {
        Vuelo vuelo = await vuelosService.RegistrarVuelo(vueloDTO);
        return Ok(vuelo);
    }

    [Authorize(Roles = "admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<Vuelo>> Update([FromBody] UpdateVueloDTO vuelo, int id)
    {
        var response = await vuelosService.ModificarVuelo(id, vuelo);
        return response.ToActionResult();
    }

    [Authorize(Roles = "admin")]
    [HttpPost("{id}/cancelar")]
    public async Task<ActionResult<Vuelo>> Cancel(int id)
    {
        var response = await vuelosService.CancelarVuelo(id);
        return response.ToActionResult();
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Eliminar(int id)
    {
        var response = await vuelosService.EliminarVuelo(id);
        return response.ToActionResult();
    }

    [Authorize(Roles = "admin")]
    [HttpPost("{id}/asignar_slot")]
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

    [Authorize(Roles = "admin")]
    [HttpPost("{id}/confirmar")]
    public async Task<ActionResult<Vuelo>> ConfirmarVuelo(int id)
    {
        var response = await vuelosService.ConfirmarVuelo(id);
        return response.ToActionResult();
    }

    [Authorize(Roles = "admin")]
    [HttpPost("{id}/programar")]
    public async Task<ActionResult<Vuelo>> ProgramarVuelo(int id)
    {
        var response = await vuelosService.ProgramarVuelo(id);
        return response.ToActionResult();
    }

    [Authorize(Roles = "admin")]
    [HttpPost("{id}/aeronave")]
    public async Task<ActionResult<Vuelo>> CambiarAeronave([FromBody] CambiarAeronaveDTO aeronaveDTO, int id)
    {
        var response = await vuelosService.AsignarAeronave(id, aeronaveDTO);
        return response.ToActionResult();
    }
}