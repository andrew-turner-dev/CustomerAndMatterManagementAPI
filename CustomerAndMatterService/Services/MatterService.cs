using CustomerAndMatterData;
using CustomerAndMatterData.Models;
using CustomerAndMatterService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceModels = CustomerAndMatterService.Models;
using DataModels = CustomerAndMatterData.Models;

namespace CustomerAndMatterService.Services
{
    public class MatterService : IMatterSerrvice
    {
        private readonly CustomerAndMatterContext _customerAndMatterContext;

        public MatterService(CustomerAndMatterContext customerAndMatterContext)
        {
            _customerAndMatterContext = customerAndMatterContext;
        }

        public async Task<List<ServiceModels.Matter>> GetMattersByCustomerIdAsync(long customerId, long lawyerId)
        {
            var res = await _customerAndMatterContext.Matters
                .Where(m => m.CustomerId == customerId
                && m.LawyerId == lawyerId)
                .Select(m => new ServiceModels.Matter
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    IsClosed = m.IsClosed,
                    CustomerId = m.CustomerId,
                    LawyerId = m.LawyerId
                })
                .ToListAsync();

            return res;
        }

        public async Task<DataModels.Matter> CreateMatterForCustomerAsync(long customerId, CustomerAndMatterService.Models.Matter matter, long lawyerId)
        {
            var customerExists = await _customerAndMatterContext.Customers.AnyAsync(c => c.Id == customerId);
            if (!customerExists)
            {
                return null;
            }

            var matterToBeAdded = new CustomerAndMatterData.Models.Matter() { Name = matter.Name, Description = matter.Description, IsClosed = matter.IsClosed, CustomerId = customerId, LawyerId=lawyerId };
            await _customerAndMatterContext.Matters.AddAsync(matterToBeAdded);
            await _customerAndMatterContext.SaveChangesAsync();
            return matterToBeAdded;
        }

        public async Task<ServiceModels.Matter?> GetMatterByCustomerIdAndMatterIdAsync(long customerId, long matterId)
        {
            var matter = await _customerAndMatterContext.Matters
                .FirstOrDefaultAsync(m => m.CustomerId == customerId && m.Id == matterId);


            return new ServiceModels.Matter
            {
                Id = matter.Id,
                Name = matter.Name,
                Description = matter.Description,
                IsClosed = matter.IsClosed,
                CustomerId = matter.CustomerId,
                LawyerId = matter.LawyerId
            };

        }
    }
}
