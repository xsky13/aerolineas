using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.Interfaces;

namespace Aerolineas.Services;

public class SlotService : ISlotService
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
        // hacer post con el slotRequest
        return Result<int>.Ok(123);
    }
}