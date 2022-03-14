using ElancoUI.Data;
using ElancoUI.Helpers;
using ElancoUI.Models;
using ElancoUI.Models.DbContextModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElancoUI.Areas.Identity.Pages.Account.Manage
{
    public class AddPetModel : PageModel
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private IAccountData _accountData;
        private IAccountHelper _accountHelper;
        private readonly IWebHostEnvironment _env;

        [BindProperty]
        public Pet NewPet { get; set; }

        [BindProperty]
        public IFormFile PetUpload { get; set; }

        public AddPetModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IAccountData accountData, IAccountHelper accountHelper, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountData = accountData;
            _accountHelper = accountHelper;
            _env = env;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            string trustedFileName = $"{ Guid.NewGuid().ToString() }_{ PetUpload.FileName }";

            // In production, this would be stored on a physical disk.
            string filePath = Path.Combine(_env.ContentRootPath, "wwwroot", "storage", "account_pets", trustedFileName);
            
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await PetUpload.CopyToAsync(fileStream);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var dbAccount = _accountData.GetAccountDetails(user);
            AccountModel account = _accountHelper.FormatAccountDetails(dbAccount);

            NewPet.ImageName = trustedFileName;
            account.Pets.Add(NewPet);
            _accountData.SaveAccountDetails(account);

            await _signInManager.RefreshSignInAsync(user);

            return RedirectToPage("Index");
        }
    }
}
