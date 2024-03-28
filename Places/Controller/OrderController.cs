using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Places.Data;
using Places.Dto;
using Places.Interfaces;
using Places.Models;
using Stripe.Climate;


namespace Places.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<Models.Order>>> GetAllOrdersByUserId(int userId)
        {
            var orders = await _orderRepository.GetAllOrdersByUserId(userId);

            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);
        }

        // PUT: api/Orders/5/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatusRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updated = await _orderRepository.UpdateOrderStatusAsync(id, request.NewStatus);
                if (!updated)
                {
                    return NotFound();
                }

                return NoContent(); // You could also return a 200 OK if you prefer
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost]
        public ActionResult AddOrder(OrderDto orderDto)
        {
            var order = new Models.Order
            {
                OrderStatus = "Comanda plasata",
                OrderDate = DateTime.Now,
                UserId = orderDto.UserId,
                AddressId = orderDto.AddressId,
                CodBSR = orderDto.CodBSR
            };

            var orderProducts = orderDto.Products.Select(p => new OrderProduct
            {
                ProductId = p.ProductId,
                Quantity = p.Quantity
            }).ToList();

            _orderRepository.AddOrder(order, orderProducts);

            return Ok();
        }
    }
}
