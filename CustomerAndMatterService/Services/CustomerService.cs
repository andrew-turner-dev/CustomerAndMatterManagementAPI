using CustomerAndMatterData;
using DataModels = CustomerAndMatterData.Models;
using CustomerAndMatterService.Interfaces;
using ServiceModels = CustomerAndMatterService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;



namespace CustomerAndMatterService.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerAndMatterContext _customerAndMatterContext;

        public CustomerService(CustomerAndMatterContext customerAndMatterContext)
        {
            _customerAndMatterContext = customerAndMatterContext;
        }

        public async Task<List<ServiceModels.Customer>> GetAllCustomersAsync(long lawyerId)
        {
             var result = await _customerAndMatterContext.Customers.Where(c => c.LawyerId == lawyerId).Select(c =>
                new ServiceModels.Customer() 
                    { 
                        Id = c.Id,
                        Name = c.Name,
                        Email = c.Email,
                        PhoneNumber = c.PhoneNumber
                    }
                 
             ).ToListAsync();


            return result;
           
        }

        public async Task<ServiceModels.Customer> GetCustomerByIdAsync(long Id)
        {
            var result = await _customerAndMatterContext.Customers.Where(c => c.Id == Id).Select(c =>
                new ServiceModels.Customer()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber
                }).FirstOrDefaultAsync();

            return result;
        }


        public async Task<bool> AddCustomerAsync(ServiceModels.Customer customer, long lawyerId)
        {
            try
            {
                var customerToBeAdded = new DataModels.Customer() { Name = customer.Name, PhoneNumber = customer.PhoneNumber, Email = customer.Email, LawyerId = lawyerId };
                await _customerAndMatterContext.AddAsync(customerToBeAdded);
                await _customerAndMatterContext.SaveChangesAsync();
            }catch(Exception ex)
            {
                throw ex;
            }
            return true;
            
        }


        public async Task<bool> UpdateCustomerAsync(long id, ServiceModels.Customer customer)
        {
            var existingCustomer = await _customerAndMatterContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (existingCustomer == null)
            {
                return false;
            }

            existingCustomer.Name = customer.Name;
            existingCustomer.PhoneNumber = customer.PhoneNumber;
            existingCustomer.Email = customer.Email;

            await _customerAndMatterContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCustomerAsync(long id)
        {
            var existingCustomer = await _customerAndMatterContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (existingCustomer == null)
            {
                return false;
            }

            _customerAndMatterContext.Customers.Remove(existingCustomer);
            await _customerAndMatterContext.SaveChangesAsync();
            return true;
        }
    }
}
