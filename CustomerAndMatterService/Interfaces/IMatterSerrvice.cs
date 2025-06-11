
using ServiceModels = CustomerAndMatterService.Models;
using DataModels = CustomerAndMatterData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAndMatterService.Interfaces
{
    public interface IMatterSerrvice
    {
        Task<DataModels.Matter> CreateMatterForCustomerAsync(long customerId, ServiceModels.Matter matter, long lawyerId);
        Task<ServiceModels.Matter?> GetMatterByCustomerIdAndMatterIdAsync(long customerId, long matterId);
        Task<List<ServiceModels.Matter>> GetMattersByCustomerIdAsync(long customerId, long lawyerId);
    }
}
