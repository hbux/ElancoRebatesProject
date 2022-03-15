using ElancoUI.Data;
using ElancoUI.Models.DbContextModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElancoUI.Areas.Identity.Pages.Account.Manage
{
    public class AddAddressModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IAccountData _accountData;

        [BindProperty]
        public Address NewAddress { get; set; }

        public AddAddressModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IAccountData accountData)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountData = accountData;
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

            ElancoUI.Models.DbContextModels.Account account = _accountData.GetAccountDetails(user);

            if (account.Addresses.Count == 0)
            {
                NewAddress.IsDefault = true;
            }

            account.Addresses.Add(NewAddress);
            _accountData.SaveAccountDetails();

            await _signInManager.RefreshSignInAsync(user);
            return RedirectToPage("Index");
        }
    }
}
