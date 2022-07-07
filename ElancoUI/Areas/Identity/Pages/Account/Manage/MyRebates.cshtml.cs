using ElancoLibrary.Data;
using ElancoLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElancoUI.Areas.Identity.Pages.Account.Manage
{
    public class MyRebatesModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<MyRebatesModel> _logger;
        private readonly IRebateData _rebateData;

        public List<FormModel> SubmittedRebates { get; set; } = new List<FormModel>();

        public MyRebatesModel(UserManager<IdentityUser> userManager, ILogger<MyRebatesModel> logger,
            IRebateData rebateData)
        {
            _userManager = userManager;
            _logger = logger;
            _rebateData = rebateData;
        }

        public async Task OnGet()
        {
            string userId = _userManager.GetUserId(User);

            try
            {
                SubmittedRebates = await _rebateData.GetAllSubmissions(userId);
            }
            catch (Exception)
            {
                // TODO: Add error modal
            }
        }
    }
}
