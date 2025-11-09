using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.Interfaces;
using Aerolineas.Models;
using Microsoft.EntityFrameworkCore;

namespace Aerolineas.Services;

public class SlotService(AeroContext db) : ISlotService
{
    public async Task<Result<SlotResponse>> GetSlot(int id)
    {
         SlotResponse slotResponse = new SlotResponse()
        {
            Id = id,
            Date = DateTime.Parse("2025-11-20T14:30:00Z"),
            Runway = "Pista 1",
            GateId = 20,
            Status = "Reservado",
            FlightCode = "AR1234",
            ReservationExpiresAt = DateTime.Parse("2025-11-20T14:45:00Z")
        };
        return Result<SlotResponse>.Ok(slotResponse);
    }

    public async Task<Result<int>> ReservarSlot(int id, SlotRequestDTO slotRequest)
    {
        // hacer post con el slotRequest y retonrar id
        return Result<int>.Ok(123);
    }

    public async Task<Result<bool>> CancelSlot(int id)
    {
        // llamar api
        // eliminar slot
        var slot = await db.Slots.FirstOrDefaultAsync(slot => slot.Id == id);
        db.Slots.Remove(slot);
        await db.SaveChangesAsync();

        return Result<bool>.Ok(true);
    }

    public async Task<Slot?> FindSlot(int slotId)
    {
        return await db.Slots.FirstOrDefaultAsync(slot => slot.SlotId == slotId);
    }

}