using Places.Dto;
using Places.Models;

namespace Places.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<OrderInfoDto>> GetOrdersForProductsCreatedByUser(int userId);
    }
}
