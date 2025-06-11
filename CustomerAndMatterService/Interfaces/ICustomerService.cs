using CustomerAndMatterService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAndMatterService.Interfaces
{
    public interface ICustomerService
    {
        Task<bool> AddCustomerAsync(Customer customer, long lawyerId);
        Task<bool> DeleteCustomerAsync(long id);
        Task<List<Customer>> GetAllCustomersAsync(long lawyerId);
        Task<Customer> GetCustomerByIdAsync(long Id);
        Task<bool> UpdateCustomerAsync(long id, Customer customer);
    }
}
