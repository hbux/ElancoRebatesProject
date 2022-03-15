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
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private IAccountData _accountData;
        private IAccountHelper _accountHelper;
        private readonly IWebHostEnvironment _env;

        public EditPetModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            IAccountData accountData, IAccountHelper accountHelper, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountData = accountData;
            _accountHelper = accountHelper;
            _env = env;
        }


        [BindProperty]
        public Pet CurrentPet { get; set; }
        
        [BindProperty]
        public IFormFile PetUpload { get; set; }

        public void OnGet(int? id)
        {
            try
            {
                CurrentPet = _accountData.GetPetById(id.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void OnPost()
        {
            
        }
    }
}
