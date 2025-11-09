
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
public class VueloController(IVuelosService vuelosService, ISlotService slotService) : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<Vuelo>>> Get()
    {
        var vuelos = await vuelosService.GetVuelosFull();
        return Ok(vuelos);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<VueloDTO>> GetById(int id)
    {
        var vuelo = await vuelosService.GetVueloFull(id);
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
    public async Task<ActionResult<VueloDTO>> Cancel(int id)
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
    public async Task<ActionResult<VueloDTO>> AsignarSlot([FromBody] SlotRequestDTO slotRequest, int id)
    {
        var slotIdResponse = await slotService.ReservarSlot(id, slotRequest);
        if (!slotIdResponse.Success) return BadRequest(new { error = slotIdResponse.Error });

        var slotResponse = await slotService.GetSlot(slotIdResponse.Value);
        if (!slotResponse.Success) return BadRequest(new { error = slotResponse.Error });

        var slot = await vuelosService.AsignarSlot(id, slotResponse.Value!);
        return slot.ToActionResult();
    }

    [Authorize(Roles = "admin")]
    [HttpPost("{id}/confirmar")]
    public async Task<ActionResult<VueloDTO>> ConfirmarVuelo(int id)
    {
        var response = await vuelosService.ConfirmarVuelo(id);
        return response.ToActionResult();
    }

    [Authorize(Roles = "admin")]
    [HttpPost("{id}/programar")]
    public async Task<ActionResult<VueloDTO>> ProgramarVuelo(int id)
    {
        var response = await vuelosService.ProgramarVuelo(id);
        return response.ToActionResult();
    }

    [Authorize(Roles = "admin")]
    [HttpPost("{id}/aeronave")]
    public async Task<ActionResult<VueloDTO>> CambiarAeronave([FromBody] CambiarAeronaveDTO aeronaveDTO, int id)
    {
        var response = await vuelosService.AsignarAeronave(id, aeronaveDTO);
        return response.ToActionResult();
    }
}