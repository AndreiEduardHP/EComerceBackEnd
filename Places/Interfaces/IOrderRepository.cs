using Places.Models;

namespace Places.Interfaces
{
    public interface IOrderRepository
    {
        void AddOrder(Order order, List<OrderProduct> orderProducts);
        Task<IEnumerable<Order>> GetAllOrdersByUserId(int userId);
        Task<bool> UpdateOrderStatusAsync(int orderId, string newStatus);

    }
}
