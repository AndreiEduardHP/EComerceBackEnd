using Microsoft.EntityFrameworkCore;
using Places.Data;
using Places.Interfaces;
using Places.Models;

namespace Places.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly PlacesContext _context;

        public OrderRepository(PlacesContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Order>> GetAllOrdersByUserId(int userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                
               
                .ToListAsync();
        }
        public async Task<bool> UpdateOrderStatusAsync(int orderId, string newStatus)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return false;
            }

            order.OrderStatus = newStatus; // Assuming OrderStatus is the name of the property
            await _context.SaveChangesAsync();
            return true;
        }
        public void AddOrder(Order order, List<OrderProduct> orderProducts)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Orders.Add(order);
                    _context.SaveChanges();

                    foreach (var orderProduct in orderProducts)
                    {
                        orderProduct.OrderId = order.Id; // Asigură-te că OrderId este setat
                        _context.OrderProducts.Add(orderProduct);
                    }
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}

