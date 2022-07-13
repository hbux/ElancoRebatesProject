using ElancoUI.Data;
using ElancoUI.Models.DbContextModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ElancoUI.Areas.Identity.Pages.Account.Manage
{
    public class AddAddressModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AddAddressModel> _logger;
        private readonly IAccountData _accountData;

        [BindProperty]
        public InputModel NewAddress { get; set; }

        public AddAddressModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IAccountData accountData, ILogger<AddAddressModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

            ElancoUI.Models.DbContextModels.Account account = _accountData.GetAccountDetails(user);

            Address address = new Address()
            {
                AddressLine1 = NewAddress.AddressLine1,
                City = NewAddress.City,
                State = NewAddress.State,
                ZipCode = NewAddress.ZipCode
            };

            if (account.Addresses.Count == 0)
            {
                address.IsDefault = true;
            }

            account.Addresses.Add(address);
            _accountData.SaveAccountDetails();

            _logger.LogInformation("User ID: {Id} created new address at {Time}", user.Id, DateTime.UtcNow);

            await _signInManager.RefreshSignInAsync(user);
            return RedirectToPage("Index");
        }

        public class InputModel
        {
            [Required]
            [MaxLength(200)]
            public string AddressLine1 { get; set; }

            [Required]
            [MaxLength(100)]
            public string City { get; set; }

            [Required]
            [MaxLength(50)]
            public string State { get; set; }

            [Required]
            [MaxLength(10)]
            public string ZipCode { get; set; }
        }
    }
}
