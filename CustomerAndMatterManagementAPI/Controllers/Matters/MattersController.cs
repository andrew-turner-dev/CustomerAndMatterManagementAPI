using CustomerAndMatterService.Interfaces;
using CustomerAndMatterData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerAndMatterManagementAPI.Controllers.Matters
{
    [Route("api/[controller]")]
    [ApiController]
    public class MattersController : ControllerBase
    {
        private readonly IMatterSerrvice _matterService;

        public MattersController(IMatterSerrvice matterService)
        {
            _matterService = matterService;
        }

        //// GET: api/Matters/customer/{customerId}
        //[HttpGet("{customerId}")]
        //public async Task<ActionResult<List<Matter>>> GetMattersByCustomerId(long customerId)
        //{
        //    var matters = await _matterService.GetMattersByCustomerIdAsync(customerId);
        //    if (matters == null || matters.Count == 0)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(matters);
        //}

        //// POST: api/Matters/customer/{customerId}
        //[HttpPost("customer/{customerId}")]
        //public async Task<ActionResult<Matter>> CreateMatterForCustomer(long customerId, [FromBody] Matter matter)
        //{
        //    var createdMatter = await _matterService.CreateMatterForCustomerAsync(customerId, matter);
        //    if (createdMatter == null)
        //    {
        //        return NotFound($"Customer with id {customerId} not found.");
        //    }
        //    return CreatedAtAction(nameof(GetMatterForCustomer), new { customerId = customerId, matterId = createdMatter.Id }, createdMatter);
        //}

        //// GET: api/Matters/customer/{customerId}/matter/{matterId}
        //[HttpGet("customer/{customerId}/matter/{matterId}")]
        //public async Task<ActionResult<Matter>> GetMatterForCustomer(long customerId, long matterId)
        //{
        //    var matter = await _matterService.GetMatterByCustomerIdAndMatterIdAsync(customerId, matterId);
        //    if (matter == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(matter);
        //}
    }
}