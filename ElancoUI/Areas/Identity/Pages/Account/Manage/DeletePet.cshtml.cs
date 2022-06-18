using ElancoUI.Data;
using ElancoUI.Models.DbContextModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ElancoUI.Areas.Identity.Pages.Account.Manage
{
    public class DeletePetModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private ILogger<DeletePetModel> _logger;
        private readonly IAccountData _accountData;

        [BindProperty]
        public Pet Pet { get; set; }
        public string ErrorMessage { get; set; }

        public DeletePetModel(UserManager<IdentityUser> userManager, IAccountData accountData, 
            ILogger<DeletePetModel> logger)
        {
            _userManager = userManager;
            _accountData = accountData;
            _logger = logger;
        }

        public void OnGet(int? id, bool? saveChangesError = false)
        {
            Pet = _accountData.GetPetByIdNotracking(id.Value);

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

            Pet pet = _accountData.GetPetById(id);

            try
            {
                _accountData.RemovePet(pet);
                _accountData.SaveAccountDetails();

                _logger.LogInformation("User ID: {Id} deleted pet at {Time}", user.Id, DateTime.UtcNow);

                return RedirectToPage("Index");
            }
            catch (DbUpdateException)
            {
                return RedirectToAction("DeletePet", new { id, saveChangesError = true });
            }
        }
    }
}
