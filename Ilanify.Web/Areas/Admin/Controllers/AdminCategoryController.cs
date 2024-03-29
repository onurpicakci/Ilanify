using Ilanify.DataAccess.Interfaces;
using Ilanify.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ilanify.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Category/[action]")]
    public class AdminCategoryController : Controller
    {
        private readonly IRepository<Category> _categoryRepository;

        public AdminCategoryController(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            await _categoryRepository.AddAsync(category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return View(category);
        }

        [HttpPost]
        [Route("{id:int}")]
        public async Task<IActionResult> Edit(Category category)
        {
            await _categoryRepository.UpdateAsync(category);
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            await _categoryRepository.DeleteAsync(category);
            return RedirectToAction("Index");
        }
    }
}