using System.Text.Json;
using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.Interfaces;
using Aerolineas.Models;
using Microsoft.EntityFrameworkCore;

namespace Aerolineas.Services;

public class SlotService(AeroContext db) : ISlotService
{

    private static readonly HttpClient httpClient;

    static SlotService()
    {
        httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5000")
        };
        httpClient.DefaultRequestHeaders.Add("X-API-Key", Environment.GetEnvironmentVariable("ExternalApiKey") ?? throw new InvalidOperationException("No hay jwtkey"));
    }

    public async Task<Result<SlotResponse>> GetSlot(int id)
    {
        // SlotResponse slotResponse = new SlotResponse()
        // {
        //     Id = id,
        //     Date = DateTime.Parse("2025-11-20T14:30:00Z"),
        //     Runway = "Pista 1",
        //     GateId = 20,
        //     Status = "Reservado",
        //     FlightCode = "AR1234",
        //     ReservationExpiresAt = DateTime.Parse("2025-11-20T14:45:00Z")
        // };
        try
        {
            var response = await httpClient.PostAsync($"api/slot/confirm/{id}", null);
            response.EnsureSuccessStatusCode();

            var slotResponse = await response.Content.ReadFromJsonAsync<SlotResponse>();
            return Result<SlotResponse>.Ok(slotResponse!);
        }
        catch (Exception ex)
        {
            return Result<SlotResponse>.Fail($"Error: {ex.Message}", 500);
        }
    }

    public async Task<Result<int>> ReservarSlot(int vueloId, SlotRequestDTO slotRequest)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("api/slot/reserve", slotRequest);
            response.EnsureSuccessStatusCode();

            var slotResponse = await response.Content.ReadFromJsonAsync<SlotResponse>();
            return Result<int>.Ok(slotResponse!.Id);
        }
        catch (Exception ex)
        {
            return Result<int>.Fail($"Error: {ex.Message}", 500);
        }
    }

    public async Task<Result<bool>> CancelSlot(int id)
    {
        // llamar api
        // eliminar slot
        var slot = await db.Slots.FirstOrDefaultAsync(slot => slot.Id == id);

        if (slot == null)
            return Result<bool>.Fail("El slot no existe", 404);

        db.Slots.Remove(slot);
        await db.SaveChangesAsync();

        return Result<bool>.Ok(true);
    }

    public async Task<Slot?> FindSlot(int slotId)
    {
        return await db.Slots.FirstOrDefaultAsync(slot => slot.SlotId == slotId);
    }

}