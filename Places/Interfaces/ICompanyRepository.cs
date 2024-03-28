using Places.Dto;
using Places.Models;

namespace Places.Interfaces
{
    public interface ICompanyRepository
    {
        Task<Company> CreateAddress(Company company);
    }
}
