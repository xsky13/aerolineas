using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.Models;

namespace Aerolineas.Interfaces;

public interface ISlotService
{
    Task<Result<int>> ReservarSlot(int vueloId, SlotRequestDTO slotRequest);
    Task<Result<SlotResponse>> GetSlot(int id);
    Task<Result<bool>> CancelSlot(int id);
    Task<Slot?> FindSlot(int slotId);
}