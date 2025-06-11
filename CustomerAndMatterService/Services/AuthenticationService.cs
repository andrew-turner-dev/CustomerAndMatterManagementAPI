using CustomerAndMatterData;

using CustomerAndMatterService.Interfaces;
using CustomerAndMatterService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAndMatterService.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly CustomerAndMatterContext _context;
        private readonly string _jwtSecret = "ASuperSecretKeyThatIsAtLeast32BytesLong!!"; // TODO: Replace with a secure key and move to config in production

        public AuthenticationService(CustomerAndMatterContext context)
        {
            _context = context;
        }

        public async Task<CustomerAndMatterData.Models.Lawyer> AddLawyerAsync(Lawyer lawyer)
        {
            // Optionally, check if a lawyer with the same email already exists
            var exists = await _context.Lawyers.AnyAsync(l => l.LoginEmail == lawyer.LoginEmail);
            if (exists)
            {
                return null; //User already exists. TODO: Handle this better
            }

            var LawyerToBeAdded = new CustomerAndMatterData.Models.Lawyer() { FirstName = lawyer.FirstName, LastName = lawyer.FirstName, LoginEmail = lawyer.LoginEmail, Password = lawyer.Password, Firm = lawyer.Firm, IsAdmin = lawyer.IsAdmin };
            await _context.Lawyers.AddAsync(LawyerToBeAdded);
            await _context.SaveChangesAsync();
            return LawyerToBeAdded;
        }


        public async Task<string> AuthenticateLawyerLogin(LawyerLogin lawyer)
        {

            var lawyerOnDataBase = await GetLawyerByEmailAsync(lawyer.LoginEmail);
            if (lawyerOnDataBase == null)
            {
                return string.Empty; // Email not found
            }

            // Todo: Hash passwords
            if (lawyerOnDataBase.Password != lawyer.Password)
            {
                return string.Empty; // Password mismatch
            }


            // Generate JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, lawyerOnDataBase.Id.ToString()),
                new Claim(ClaimTypes.Name, lawyerOnDataBase.FirstName + " " + lawyerOnDataBase.LastName),
                new Claim(ClaimTypes.Email, lawyerOnDataBase.LoginEmail),
                new Claim("Firm", lawyerOnDataBase.Firm ?? ""),
                new Claim("IsAdmin", lawyerOnDataBase.IsAdmin.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = "ApiIssuer",      // TODO: pull from appsettings.json
                Audience = "LawyerAudience",  // TODO: pull from appsettings.json
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

            
        }



        public async Task<CustomerAndMatterData.Models.Lawyer?> GetLawyerByEmailAsync(string email)
        {
            return await _context.Lawyers
                .FirstOrDefaultAsync(l => l.LoginEmail == email);
        }
    }
}
