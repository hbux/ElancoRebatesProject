using ElancoUI.Data;
using ElancoUI.Helpers;
using ElancoUI.Models;
using ElancoUI.Models.DbContextModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElancoUI.Areas.Identity.Pages.Account.Manage
{
    public class EditPetModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<EditPetModel> _logger;
        private readonly IAccountData _accountData;

        [BindProperty]
        public Pet Pet { get; set; }

        [BindProperty]
        public IFormFile PetUpload { get; set; }

        public EditPetModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IWebHostEnvironment env, IAccountData accountData, ILogger<EditPetModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
            _accountData = accountData;
            _logger = logger;
        }

        public void OnGet(int? id)
        {
            Pet = _accountData.GetPetById(id.Value);
        }

        public async Task<IActionResult> OnPost(int id)
        {
            Pet petToUpdate = _accountData.GetPetById(id);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (PetUpload != null)
            {
                string trustedFileName = $"{ Guid.NewGuid() }_{ PetUpload.FileName }";
                string filePath = Path.Combine(_env.ContentRootPath, "wwwroot", "storage", "account_pets", trustedFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await PetUpload.CopyToAsync(fileStream);
                }

                petToUpdate.ImageName = trustedFileName;

            }

            if (await TryUpdateModelAsync<Pet>(petToUpdate, "pet", p => p.Name, p => p.ImageName))
            {
                _accountData.SaveAccountDetails();
            }

            _logger.LogInformation("User ID: {Id} edited pet at {Time}", user.Id, DateTime.UtcNow);

            await _signInManager.RefreshSignInAsync(user);
            return RedirectToPage("Index");
        }
    }
}
