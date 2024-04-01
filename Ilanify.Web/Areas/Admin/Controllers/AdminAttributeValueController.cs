using Ilanify.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ilanify.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AttributeValue/[action]")]
    public class AdminAttributeValueController : Controller
    {
       private readonly IAttributeValueService _attributeValueService;
       private readonly ICategoryAttributeService _categoryAttributeService;

        public AdminAttributeValueController(IAttributeValueService attributeValueService, ICategoryAttributeService categoryAttributeService)
        {
            _attributeValueService = attributeValueService;
            _categoryAttributeService = categoryAttributeService;
        }

        public async Task<IActionResult> Index()
        {
            var attributeValues = await _attributeValueService.GetAllAttributeValuesAsync();
            return View(attributeValues);
        }
    }
}
