using ElancoUI.Models.DbContextModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElancoUI.Areas.Identity.Pages.Account.Manage
{
    public class AddPetModel : PageModel
    {
        [BindProperty]
        public Pet NewPet { get; set; }

        public void OnGet()
        {

        }

        public void OnPost()
        {

        }
    }
}
