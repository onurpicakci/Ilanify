using Ilanify.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ilanify.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[action]")]
    public class AdminHomeController : Controller
    {
        private readonly IRealEstateService _realEstateService;

        public AdminHomeController(IRealEstateService realEstateService)
        {
            _realEstateService = realEstateService;
        }

        public async Task<IActionResult> Index()
        {
            var realEstatesCount = await _realEstateService.GetRealEstatesCount();
            return View(realEstatesCount);
        }
    }
}
