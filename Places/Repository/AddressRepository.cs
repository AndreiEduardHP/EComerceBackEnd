using Microsoft.EntityFrameworkCore;
using Places.Data;
using Places.Dto;
using Places.Interfaces;
using Places.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Places.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly PlacesContext _context;

        public AddressRepository(PlacesContext context)
        {
            _context = context;
        }

        public async Task<Address> CreateAddress(AddressDto addressDto)
        {
            var address = new Address
            {
                City = addressDto.City,
                Details = addressDto.Details,
                ContactPhoneNumber = addressDto.ContactPhoneNumber,
                UserId = addressDto.UserId
            };

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return address;
        }

        public async Task<Address> GetAddressById(int id)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Address>> GetAllAddressesByUserId(int userId)
        {
            return await _context.Addresses
                .Where(a => a.UserId == userId && a.IsDisabled != true)
                .ToListAsync();
        }

        public async Task UpdateAddress(int id, AddressUpdateDto addressUpdateDto)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                throw new Exception("Address not found");
            }

            address.City = addressUpdateDto.City;
            address.Details = addressUpdateDto.Details;
            address.ContactPhoneNumber = addressUpdateDto.ContactPhoneNumber;
            

            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAddress(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address != null)
            {
                address.IsDisabled = true;
               
                await _context.SaveChangesAsync();
            }
        }

     
    }
}
