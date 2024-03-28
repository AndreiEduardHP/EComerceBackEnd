using Microsoft.AspNetCore.Mvc;
using Places.Dto;
using Places.Interfaces;
using Places.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Places.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressRepository _addressRepository;

        public AddressesController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        // POST: api/Addresses
        [HttpPost]
        public async Task<ActionResult> CreateAddress([FromBody] AddressDto addressDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           await _addressRepository.CreateAddress(addressDto);
            return Ok();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAddressById(int id)
        {
            var address = await _addressRepository.GetAddressById(id);
            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        // GET: api/Addresses/user/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Address>>> GetAllAddressesByUserId(int userId)
        {
            var addresses = await _addressRepository.GetAllAddressesByUserId(userId);
            return Ok(addresses);
        }

        // PUT: api/Addresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] AddressUpdateDto addressUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _addressRepository.UpdateAddress(id, addressUpdateDto);
            }
            catch (System.Exception)
            {
                var addressExists = await _addressRepository.GetAddressById(id) != null;
                if (!addressExists)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var address = await _addressRepository.GetAddressById(id);
            if (address == null)
            {
                return NotFound();
            }

            await _addressRepository.DeleteAddress(id);
            return NoContent();
        }
    }
}
