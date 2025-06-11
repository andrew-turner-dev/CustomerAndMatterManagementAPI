using CustomerAndMatterData.Models;
using CustomerAndMatterService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAndMatterService.Interfaces
{
    public interface IAuthenticationService
    {
        Task<CustomerAndMatterData.Models.Lawyer> AddLawyerAsync(Models.Lawyer lawyer);
        Task<string> AuthenticateLawyerLogin(Models.LawyerLogin lawyer);
        Task<CustomerAndMatterData.Models.Lawyer?> GetLawyerByEmailAsync(string email);
    }
}
