using Aerolineas.Config;
using Aerolineas.DTO;

namespace Aerolineas.Interfaces;

public interface ISlotService
{
    Task<Result<int>> ReservarSlot(int vueloId, SlotRequestDTO slotRequest);
    Task<Result<SlotResponse>> GetSlot(int id);
}