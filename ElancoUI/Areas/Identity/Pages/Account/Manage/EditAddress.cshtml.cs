using ElancoUI.Data;
using ElancoUI.Models.DbContextModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElancoUI.Areas.Identity.Pages.Account.Manage
{
    public class EditAddressModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<EditAddressModel> _logger;
        private readonly IAccountData _accountData;

        [BindProperty]
        public Address Address { get; set; }

        public EditAddressModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IAccountData accountData, ILogger<EditAddressModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountData = accountData;
            _logger = logger;
        }

        public void OnGet(int? id)
        {
            Address = _accountData.GetAddressById(id.Value);
        }

        public async Task<IActionResult> OnPost(int id)
        {
            Address addressToUpdate = _accountData.GetAddressById(id);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (Address.IsDefault == true)
            {
                _accountData.GetAccountDetails(user).Addresses.ForEach(a => a.IsDefault = false);
            }

            if (await TryUpdateModelAsync<Address>(addressToUpdate, "address", 
                a => a.AddressLine1, a => a.City, a => a.State, a => a.ZipCode, a => a.IsDefault))
            {
                _accountData.SaveAccountDetails();
            }

            _logger.LogInformation("User ID: {Id} edited address at {Time}", user.Id, DateTime.UtcNow);

            await _signInManager.RefreshSignInAsync(user);
            return RedirectToPage("Index");
        }
    }
}
