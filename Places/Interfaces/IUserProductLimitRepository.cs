using Places.Dto;
using Places.Models;

namespace Places.Interfaces
{
    public interface IUserProductLimitRepository
    {
        Task AddLimit(UserProductLimitDto userProductLimit);
        Task<List<UserProductLimit>> GetUserProductLimitsWithDetails();

        Task<bool> EditLimit(int limitId, int newLimit);
    }
}
