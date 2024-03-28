using Ilanify.Application.Interfaces;
using Ilanify.DataAccess.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Ilanify.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealEstateFilterController : ControllerBase
    {
        private readonly IRealEstateService _realEstateService;

        public RealEstateFilterController(IRealEstateService realEstateService)
        {
            _realEstateService = realEstateService;
        }
        
        [HttpPost]
        public async Task<IActionResult> GetRealEstatesByFilter([FromBody] RealEstateFilter filter)
        {
            if (filter == null)
            {
                return BadRequest();
            }
            
            var realEstates = await _realEstateService.GetRealEstatesByFilterAsync(filter);
            return Ok(realEstates);
        }
    }
}
