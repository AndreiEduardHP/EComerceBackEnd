using Microsoft.AspNetCore.Mvc;
using Places.Dto;
using Places.Interfaces;
using Places.Models;
using Places.Repository;
using System.Web.Http.ModelBinding;

namespace Places.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCompany([FromBody] Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _companyRepository.CreateAddress(company);
            return Ok(company);
        }
    }
}
