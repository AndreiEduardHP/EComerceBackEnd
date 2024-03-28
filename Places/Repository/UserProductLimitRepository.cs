using Microsoft.EntityFrameworkCore;
using Places.Data;
using Places.Dto;
using Places.Interfaces;
using Places.Models;

namespace Places.Repository
{
    public class UserProductLimitRepository : IUserProductLimitRepository
    {
        private readonly PlacesContext _context;

        public UserProductLimitRepository(PlacesContext context)
        {
            _context = context;
        }

        public async Task AddLimit(UserProductLimitDto userProductLimit)
        {
            var userProductLimitNew = new UserProductLimit
            {
                UserId = userProductLimit.UserId,
                ProductId = userProductLimit.ProductId,
                Limit = userProductLimit.Limit,
                Count = userProductLimit.Count
            };

            _context.UserProductLimits.Add(userProductLimitNew);
            
            await _context.SaveChangesAsync();
        }
        public async Task<List<UserProductLimit>> GetUserProductLimitsWithDetails()
        {
            return await _context.UserProductLimits
         .Include(up => up.User)
         .Include(up => up.Product)
         .ToListAsync();

        }

        public async Task<bool> EditLimit(int limitId,int newLimit)
        {
            var limit = _context.UserProductLimits.FirstOrDefault(lim=> lim.Id == limitId);
            if (limit != null)
            {
                limit.Limit = newLimit;
                _context.SaveChanges();
                return true;
            }
            return false;

        }
    }
}
