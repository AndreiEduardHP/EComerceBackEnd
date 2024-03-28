using Places.Dto;
using Places.Models;

namespace Places.Interfaces
{
    public interface IAddressRepository
    {
        Task<Address> CreateAddress(AddressDto addressDto);
        Task<Address> GetAddressById(int id);
        Task<IEnumerable<Address>> GetAllAddressesByUserId(int userId);
        Task UpdateAddress(int id, AddressUpdateDto addressUpdateDto);
        Task DeleteAddress(int id);
    }
}
