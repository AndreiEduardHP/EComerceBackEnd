using Places.Data;
using Places.Dto;
using Places.Interfaces;
using Places.Models;
using Stripe;
using System.Net;

namespace Places.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly PlacesContext _context;

        public CompanyRepository(PlacesContext context)
        {
            _context = context;
        }

        public async Task<Company> CreateAddress(Company company)
        {
            _context.Company.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }
    }
}
