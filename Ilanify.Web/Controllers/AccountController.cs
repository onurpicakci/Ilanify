using Ilanify.Application.Interfaces;
using Ilanify.Domain.Entities;
using Ilanify.Domain.Enums;
using Ilanify.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ilanify.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRealEstateService _realEstateService;
        private readonly ICategoryService _categoryService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IRealEstateService realEstateService, ICategoryService categoryService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _realEstateService = realEstateService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "RealEstate");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Username,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "RealEstate");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "RealEstate");
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            var model = new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ProfileImageUrl = user.ProfileImageUrl
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;

                if (!string.IsNullOrWhiteSpace(model.Password) && model.Password == model.ConfirmPassword)
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
                }

                if (model.ProfileImage != null && model.ProfileImage.Length > 0)
                {
                    var fileName = Path.GetFileName(model.ProfileImage.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/profile-pictures",
                        fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ProfileImage.CopyToAsync(fileStream);
                    }

                    user.ProfileImageUrl = fileName;
                }

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "RealEstate");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
        
        [HttpGet]
        public async Task<IActionResult> ActivateRealEstates()
        {
            var user = await _userManager.GetUserAsync(User);
            var realEstates = await _realEstateService.GetRealEstatesByUserIdAsync(user.Id);
            
            return View(realEstates);
        }

        [HttpPost]
        public async Task<IActionResult> ActivateRealEstates(int id)
        {
            var realEstate = await _realEstateService.GetByIdAsync(id);
            
            if(realEstate == null)
            {
                return NotFound();
            }
            
            realEstate.IsActive = true;
            await _realEstateService.UpdateAsync(realEstate);
            
            return RedirectToAction("ActivateRealEstates");
        }
        
        [HttpGet]
        public async Task<IActionResult> InactiveRealEstates()
        {
            var user = await _userManager.GetUserAsync(User);
            var realEstates = await _realEstateService.GetRealEstatesByUserIdAsync(user.Id, false);
            
            return View(realEstates);
        }
        
        [HttpPost]
        public async Task<IActionResult> InactiveRealEstates(int id)
        {
            var realEstate = await _realEstateService.GetByIdAsync(id);
            
            if(realEstate == null)
            {
                return NotFound();
            }
            
            realEstate.IsActive = false;
            await _realEstateService.UpdateAsync(realEstate);
            
            return RedirectToAction("InactiveRealEstates");
        }
        
        [HttpGet]
        public async Task<IActionResult> EditRealEstate(int id)
        {
            var realEstate = await _realEstateService.GetByIdAsync(id);
            
            if(realEstate == null)
            {
                return NotFound();
            }
            
            var attributeValues = realEstate.AttributeValues.Select(av => new AttributeValueEditViewModel
            {
                Id = av.AttributeValueId,
                Value = av.Value,
                CategoryAttributeId = av.CategoryAttributeId,
                RealEstateId = av.RealEstateId,
                CategoryAttributeName = av.CategoryAttribute.Name
            }).ToList();
            
            ViewBag.Categories = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
            ViewBag.Types = Enum.GetValues(typeof(RealEstateType)).Cast<RealEstateType>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();
            
            
            var viewModel = new RealEstateEditViewModel
            {
                Id = realEstate.Id,
                Title = realEstate.Title,
                Description = realEstate.Description,
                Price = realEstate.Price,
                CategoryId = realEstate.CategoryId,
                SquareMeters = realEstate.SquareMeters,
                City = realEstate.Location.City,
                District = realEstate.Location.District,
                Neighborhood = realEstate.Location.Neighborhood,
                Type = realEstate.Type,
                AttributeValues = attributeValues
            };
            
            return View(viewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> EditRealEstate(RealEstateEditViewModel realEstate)
        {
            if (ModelState.IsValid)
            {
                var entity = await _realEstateService.GetByIdAsync(realEstate.Id);
                entity.Title = realEstate.Title;
                entity.Price = realEstate.Price;
                entity.Description = realEstate.Description;
                entity.CategoryId = realEstate.CategoryId;
                entity.SquareMeters = realEstate.SquareMeters;
                entity.UpdatedDate = DateTime.Now;
                entity.Location.City = realEstate.City;
                entity.Location.District = realEstate.District;
                entity.Location.Neighborhood = realEstate.Neighborhood;
                entity.Type = realEstate.Type;
                entity.AttributeValues = realEstate.AttributeValues.Select(av => new AttributeValue
                {
                    AttributeValueId = av.Id,
                    Value = av.Value,
                    CategoryAttributeId = av.CategoryAttributeId,
                    RealEstateId = av.RealEstateId
                }).ToList();
                
                await _realEstateService.UpdateAsync(entity);
                return RedirectToAction(nameof(ActivateRealEstates));
            }
            
            return View(realEstate);
        }
        
        public async Task<IActionResult> DeleteRealEstate(int id)
        {
            var realEstate = await _realEstateService.GetByIdAsync(id);
            
            if(realEstate == null)
            {
                return NotFound();
            }
            
            await _realEstateService.DeleteAsync(realEstate);
            
            return RedirectToAction("ActivateRealEstates");
        }
    }
}