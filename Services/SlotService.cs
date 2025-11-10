using System.Text.Json;
using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.Interfaces;
using Aerolineas.Models;
using Microsoft.EntityFrameworkCore;

namespace Aerolineas.Services;

public class SlotService(AeroContext db) : ISlotService
{

    private static readonly JsonSerializerOptions jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
    };
    private static readonly HttpClient httpClient;

    static SlotService()
    {
        httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://9plfs7gd-5000.brs.devtunnels.ms")
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
            var response = await httpClient.PostAsync($"/api/slot/confirm/{id}", null);
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
            var response = await httpClient.PostAsJsonAsync("/api/slot/reserve", slotRequest);

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"[DEBUG] Raw response ({(int)response.StatusCode}): {content}");
            Console.WriteLine($"ReservarSlot response: {content}");

            if (!response.IsSuccessStatusCode)
            {
                try
                {
                    var errorObj = JsonSerializer.Deserialize<Dictionary<string, string>>(content, jsonOptions);
                    if (errorObj != null && errorObj.ContainsKey("message"))
                    {
                        return Result<int>.Fail($"Error al reservar slot: {errorObj["message"]}", (int)response.StatusCode);
                    }
                }
                catch
                {
                    // fallback if it's not valid JSON
                }

                return Result<int>.Fail($"Error al reservar slot: {content}", (int)response.StatusCode);
            }

            // Success path
            var slotResponse = JsonSerializer.Deserialize<SlotResponse>(content, jsonOptions);
            if (slotResponse == null)
            {
                return Result<int>.Fail("Respuesta inválida del servidor", 500);
            }

            return Result<int>.Ok(slotResponse.Id);
        }
        catch (Exception ex)
        {
            return Result<int>.Fail($"Excepción: {ex.Message}", 500);
        }
    }

    public async Task<Result<bool>> CancelSlot(int id)
    {
        // llamar api
        var response = await httpClient.PostAsync($"/api/slot/cancel/{id}", null);
        var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"ReservarSlot response: {content}");
        // eliminar slot
        if (response.IsSuccessStatusCode)
        {
            var slot = await db.Slots.FirstOrDefaultAsync(slot => slot.SlotId == id);

            if (slot == null)
                return Result<bool>.Fail("El slot no existe", 404);

            db.Slots.Remove(slot);
            await db.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }
        else
        {
            return Result<bool>.Ok(false);
        }
    }

    public async Task<Slot?> FindSlot(int slotId)
    {
        return await db.Slots.FirstOrDefaultAsync(slot => slot.SlotId == slotId);
    }

}