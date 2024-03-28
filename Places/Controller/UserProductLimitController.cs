using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Places.Dto;
using Places.Interfaces;
using Places.Models;

namespace Places.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProductLimitController : ControllerBase
    {
        private readonly IUserProductLimitRepository _userProductLimitRepository;

        public UserProductLimitController(IUserProductLimitRepository userProductLimitRepository)
        {
            _userProductLimitRepository = userProductLimitRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddUserProductLimit([FromBody] UserProductLimitDto userProductLimit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _userProductLimitRepository.AddLimit(userProductLimit);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "A problem occurred while handling your request.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<UserProductLimit>>> GetLimitDetails()
        {
            var userProductLimits = await _userProductLimitRepository.GetUserProductLimitsWithDetails();
            if (userProductLimits == null || userProductLimits.Count == 0) 
            {
                return NotFound(); 
            }

            return userProductLimits;
        }

        // PUT api/userproductlimits/5/limit
        [HttpPut("{limitId}/limit")]
        public async Task<IActionResult> EditLimit(int limitId, [FromBody] UserProductLimitDto limitDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result = await _userProductLimitRepository.EditLimit(limitId, limitDto.Limit);
            if (result)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
