using ElancoUI.Data;
using ElancoUI.Models.DbContextModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ElancoUI.Areas.Identity.Pages.Account.Manage
{
    public class AddPetModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<AddPetModel> _logger;
        private readonly IAccountData _accountData;

        [BindProperty]
        public InputModel NewPet { get; set; }

        public AddPetModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IWebHostEnvironment env, IAccountData accountData, ILogger<AddPetModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
            _accountData = accountData;
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            try
            {
                string trustedFileName = $"{Guid.NewGuid()}_{NewPet.PetUpload.FileName}";
                string filePath = Path.Combine("wwwroot", "storage", "account_pets", trustedFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await NewPet.PetUpload.CopyToAsync(fileStream);
                }

                Pet pet = new Pet
                {
                    Name = NewPet.PetName,
                    ImageName = trustedFileName,
                };

                ElancoUI.Models.DbContextModels.Account account = _accountData.GetAccountDetails(user);
                account.Pets.Add(pet);
                _accountData.SaveAccountDetails();
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Upload failed.");
                return Page();
            }

            _logger.LogInformation("User ID: {Id} created new pet at {Time}", user.Id, DateTime.UtcNow);

            await _signInManager.RefreshSignInAsync(user);
            return RedirectToPage("Index");
        }

        public class InputModel
        {
            [Required]
            [MaxLength(50)]
            public string PetName { get; set; }

            [Required]
            public IFormFile PetUpload { get; set; } 
        }
    }
}
