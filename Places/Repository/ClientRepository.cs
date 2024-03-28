using Microsoft.EntityFrameworkCore;
using Places.Data;
using Places.Dto;
using Places.Interfaces;
using Places.Models;
using Stripe.Climate;

namespace Places.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly PlacesContext _context;

        public ClientRepository(PlacesContext context)
        {
            _context = context;
        }

      public async Task<IEnumerable<OrderInfoDto>> GetOrdersForProductsCreatedByUser(int userId)
        {
            var ordersWithProducts = from o in _context.Orders
                                     join op in _context.OrderProducts on o.Id equals op.OrderId
                                     join p in _context.Products on op.ProductId equals p.Id
                                     where p.CreatedByUserId == userId
                                     join u in _context.UserProfile on o.UserId equals u.Id
                                     join a in _context.Addresses on o.AddressId equals a.Id
                                     select new
                                     {
                                         Order = o,
                                         Product = p,
                                         User = u,
                                         Address = a,
                                         OrderProduct = op
                                     };

            var orderGroups = from op in ordersWithProducts
                              group op by op.Order.Id into grouped
                              select new OrderInfoDto
                              {
                                  OrderId = grouped.Key,
                                  OrderStatus = grouped.FirstOrDefault().Order.OrderStatus,
                                  OrderDate = grouped.FirstOrDefault().Order.OrderDate,
                                  Customer = new UserProfileDto
                                  {
                                      Id = grouped.FirstOrDefault().User.Id,
                                      FirstName = grouped.FirstOrDefault().User.FirstName,
                                      Email = grouped.FirstOrDefault().User.Email,
                                      PhoneNumber = grouped.FirstOrDefault().User.PhoneNumber,
                                      LastName = grouped.FirstOrDefault().User.LastName,
                                      Company = grouped.FirstOrDefault().User.Company
                                  },
                                  Address = new AddressDto
                                  {
                                 
                                      Details = grouped.FirstOrDefault().Address.Details,
                                      City = grouped.FirstOrDefault().Address.City,
                                      ContactPhoneNumber = grouped.FirstOrDefault().Address.ContactPhoneNumber

                                  },
                                  Products = grouped.Select(g => new ProductDto
                                  {
                                      ProductId = g.Product.Id,
                                      Name = g.Product.Name,
                                      Quantity = grouped.FirstOrDefault().OrderProduct.Quantity


                                  }).ToList()
                              };

            return await orderGroups.ToListAsync();
        }
    }
}
