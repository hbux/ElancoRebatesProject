using ElancoUI.Data;
using ElancoUI.Models.DbContextModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public Pet NewPet { get; set; }

        [BindProperty]
        public IFormFile PetUpload { get; set; }

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
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            string trustedFileName = $"{ Guid.NewGuid() }_{ PetUpload.FileName }";
            string filePath = Path.Combine("wwwroot", "storage", "account_pets", trustedFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await PetUpload.CopyToAsync(fileStream);
            }

            NewPet.ImageName = trustedFileName;

            ElancoUI.Models.DbContextModels.Account account = _accountData.GetAccountDetails(user);
            account.Pets.Add(NewPet);
            _accountData.SaveAccountDetails();

            _logger.LogInformation("User ID: {Id} created new pet at {Time}", user.Id, DateTime.UtcNow);

            await _signInManager.RefreshSignInAsync(user);
            return RedirectToPage("Index");
        }
    }
}
