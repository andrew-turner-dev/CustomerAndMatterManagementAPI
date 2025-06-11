using CustomerAndMatterData.Models;
using CustomerAndMatterService.Interfaces;
using CustomerAndMatterService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CustomerAndMatterManagementAPI.Controllers.Customers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMatterSerrvice _matterService;

        public CustomersController(ICustomerService customerService, IMatterSerrvice matterService, IAuthenticationService authenticationService)
        {
            _customerService = customerService;
            _matterService = matterService;
        }


        [HttpGet]
        public async Task<List<CustomerAndMatterService.Models.Customer>> GetAllCustomersAsync()
        {
            var lawyer = HttpContext.Items["Lawyer"] as CustomerAndMatterData.Models.Lawyer;
            return await _customerService.GetAllCustomersAsync(lawyer.Id);
        }

        [HttpGet("{Id}")]
        public async Task<CustomerAndMatterService.Models.Customer> GetCystomerByIdAsync(long Id)
        {
            return await _customerService.GetCustomerByIdAsync(Id);
        }

        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] CustomerAndMatterService.Models.Customer customer)
        {
            var lawyer = HttpContext.Items["Lawyer"] as CustomerAndMatterData.Models.Lawyer;
            var successfullyAddedCustomer = await _customerService.AddCustomerAsync(customer, lawyer.Id);
            if (successfullyAddedCustomer)
            {
                return CreatedAtAction("PostCustomer", customer);
            }
            else
            {
                //TODO: Better error handling
                return StatusCode(500);
            }
            
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateCustomer(long Id, [FromBody] CustomerAndMatterService.Models.Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            var existingCustomer = await _customerService.GetCustomerByIdAsync(Id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.Name = customer.Name;
            existingCustomer.PhoneNumber = customer.PhoneNumber;
            existingCustomer.Email = customer.Email;

            var updateResult = await _customerService.UpdateCustomerAsync(Id, existingCustomer);
            if (updateResult)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCustomer(long Id)
        {
            var deleted = await _customerService.DeleteCustomerAsync(Id);
            if (deleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }


        // GET: api/Customers/{customerId}/matters
        [HttpGet("{customerId}/matters")]
        public async Task<ActionResult<List<CustomerAndMatterService.Models.Matter>>> GetMattersByCustomerId(long customerId)
        {
            var lawyer = HttpContext.Items["Lawyer"] as CustomerAndMatterData.Models.Lawyer;
            var matters = await _matterService.GetMattersByCustomerIdAsync(customerId, lawyer.Id);
            if (matters == null || matters.Count == 0)
            {
                return NotFound();
            }
            return Ok(matters);
        }

        // POST: api/Customers/{customerId}/matters
        [HttpPost("{customerId}/matters")]
        public async Task<ActionResult<CustomerAndMatterService.Models.Matter>> CreateMatterForCustomer(long customerId, [FromBody] CustomerAndMatterService.Models.Matter matter)
        {
            var lawyer = HttpContext.Items["Lawyer"] as CustomerAndMatterData.Models.Lawyer;

            var createdMatter = await _matterService.CreateMatterForCustomerAsync(customerId, matter, lawyer.Id);
            if (createdMatter == null)
            {
                return NotFound($"Customer with id {customerId} not found.");
            }
            return CreatedAtAction(nameof(GetMatterForCustomer), new { customerId = customerId, matterId = createdMatter.Id }, createdMatter);
        }

        // GET: api/Customers/{customerId}/matters/{matterId}
        [HttpGet("{customerId}/matters/{matterId}")]
        public async Task<ActionResult<CustomerAndMatterService.Models.Matter>> GetMatterForCustomer(long customerId, long matterId)
        {
            var matter = await _matterService.GetMatterByCustomerIdAndMatterIdAsync(customerId, matterId);
            if (matter == null)
            {
                return NotFound();
            }
            return Ok(matter);
        }

    }
}
