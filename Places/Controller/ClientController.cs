using Microsoft.AspNetCore.Mvc;
using Places.Dto;
using Places.Interfaces;
using Places.Repository;
using System.Security.Claims;

namespace Places.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet("{userId}")]
  
        public async Task<ActionResult<IEnumerable<OrderInfoDto>>> GetOrdersForMyProducts(int userId)
        {
            

       
            var orderInfos = await _clientRepository.GetOrdersForProductsCreatedByUser(userId);

         
            return Ok(orderInfos);
        }
    }
}
