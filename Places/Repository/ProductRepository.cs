using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Places.Data;
using Places.Dto;
using Places.Interfaces;
using Places.Models;

namespace Places.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly PlacesContext _context;

        public ProductRepository(PlacesContext context)
        {
            _context = context;
        }

        public async Task AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return _context.Products.OrderBy(p => p.Id).ToList();
        }
        public async Task<List<ProductDto>> GetProductsByOrderId(int orderId)
        {
            return await _context.OrderProducts
                .Where(op => op.OrderId == orderId)
                .Select(op => new ProductDto
                {
                    ProductId = op.ProductId,
                    Name = op.Product.Name,
                    Quantity = op.Quantity
                }).ToListAsync();
        }

        public async Task<List<Product>> GetMyProducts(int userId)
        {
            return await _context.Products
                .Where(p => p.CreatedByUserId == userId)
                .ToListAsync();
        }
        public Product GetProductById(int productId)
        {
            return _context.Products.Where(p => p.Id == productId).FirstOrDefault();
        }
        public bool UpdateProductAvailability(int productId, bool isAvailable)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                product.Available = isAvailable;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public async Task UpdateProductAsync(int productId,EditProductDto product)
        {
            var foundProduct = await _context.Products.FindAsync(productId);

        
            foundProduct.Name = product.Name;
            foundProduct.ImageUrl = product.ImageUrl;
            foundProduct.Description = product.Description;
            foundProduct.Category = product.Category;
            foundProduct.Stoc = product.Stoc;
            foundProduct.Available = product.Available;
            foundProduct.ProductCod = product.ProductCod;
            foundProduct.InternalClientCod = product.InternalClientCod;

           
            _context.Products.Update(foundProduct);

            await _context.SaveChangesAsync();
        }
    }
}
