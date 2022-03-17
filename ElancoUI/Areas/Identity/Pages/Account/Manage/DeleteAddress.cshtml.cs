using ElancoUI.Data;
using ElancoUI.Models.DbContextModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ElancoUI.Areas.Identity.Pages.Account.Manage
{
    public class DeleteAddressModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAccountData _accountData;

        [BindProperty]
        public Address Address { get; set; }
        public string ErrorMessage { get; set; }

        public DeleteAddressModel(UserManager<IdentityUser> userManager, IAccountData accountData)
        {
            _userManager = userManager;
            _accountData = accountData;
        }

        public void OnGet(int? id, bool? saveChangesError = false)
        {
            Address = _accountData.GetAddressByIdNotracking(id.Value);

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = String.Format("Delete {ID} failed. Try again", id);
            }
        }

        public async Task<IActionResult> OnPost(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            Address address = _accountData.GetAddressById(id);

            try
            {
                _accountData.RemoveAddress(address);
                _accountData.SaveAccountDetails();

                return RedirectToPage("Index");
            }
            catch (DbUpdateException)
            {
                return RedirectToAction("DeletePet", new { id, saveChangesError = true });
            }
        }
    }
}
