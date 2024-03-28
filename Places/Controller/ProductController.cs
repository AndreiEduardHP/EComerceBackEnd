using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Places.Dto;
using Places.Interfaces;
using Places.Models;
using Places.Repository;

namespace Places.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            try
            {
                
                await _productRepository.AddProduct(product);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await  _productRepository.GetProducts();
                if (products == null || !products.Any())
                {
                    return NotFound("No products found.");
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
              
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{productId}/availability")]
        public IActionResult UpdateProductAvailability(int productId, [FromBody] bool isAvailable)
        {
            try
            {
                var result = _productRepository.UpdateProductAvailability(productId, isAvailable);
                if (result)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("my-products/{userId}")]
        public async Task<IActionResult> GetMyProducts(int userId)
        {
            try
            {
                var products = await _productRepository.GetMyProducts(userId);
                if (products == null || !products.Any())
                {
                    return NotFound("No products found.");
                }
                return Ok(products);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("order-details/{orderId}")] 
        public async Task<IActionResult> GetProductsByOrderId(int orderId)
        {
            var products = await _productRepository.GetProductsByOrderId(orderId);
            if (products == null || !products.Any())
            {
                return NotFound();
            }
            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{productId}")]
        public ActionResult<Product> GetProductById(int productId)
        {
            var product = _productRepository.GetProductById(productId);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] EditProductDto productDto)
        {
           

            try
            {
                await _productRepository.UpdateProductAsync(id,productDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while updating the product.");
            }
        }
    }
}
