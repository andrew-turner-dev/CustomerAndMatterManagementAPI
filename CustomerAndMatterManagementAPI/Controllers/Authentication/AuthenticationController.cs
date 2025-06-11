
using CustomerAndMatterService.Interfaces;
using CustomerAndMatterService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CustomerAndMatterManagementAPI.Controllers.Authentication
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        //Allow anonymous is used on the following two endpoint because they are not behind authentication
        // POST: api/auth/signup
        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] Lawyer request)
        {
            await _authenticationService.AddLawyerAsync(request);   
            return CreatedAtAction(nameof(SignUp), new { email = request.LoginEmail }, null);
        }

        // POST: api/auth/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LawyerLogin request)
        {
            var lawyerJwt = await _authenticationService.AuthenticateLawyerLogin(request);
            return Ok(new { Token = lawyerJwt });
        }

        // GET: api/auth/me
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            // Extract the email (or id) from the JWT claims
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized();
            }

            var lawyer = await _authenticationService.GetLawyerByEmailAsync(email);
            if (lawyer == null)
            {
                return NotFound();
            }

            // Return only safe lawyer info
            return Ok(new
            {
                lawyer.FirstName,
                lawyer.LastName,
                lawyer.Firm,
                lawyer.LoginEmail,
                lawyer.IsAdmin
            });
        }
    }
}
