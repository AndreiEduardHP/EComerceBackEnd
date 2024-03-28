using Microsoft.AspNetCore.Mvc;
using Places.Dto;
using Places.Models;

namespace Places.Interfaces
{
    public interface IProductRepository
    {

        Task AddProduct(Product product);
        Task<IEnumerable<Product>> GetProducts();

        Task<List<ProductDto>> GetProductsByOrderId(int orderId);

        Task<List<Product>> GetMyProducts(int userId);

        Product GetProductById(int productId);

         Task UpdateProductAsync(int productId, EditProductDto product);

        bool UpdateProductAvailability(int productId, bool isAvailable);
    }
}
