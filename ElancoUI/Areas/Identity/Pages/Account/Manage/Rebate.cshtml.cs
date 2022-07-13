using ElancoLibrary.Data;
using ElancoLibrary.Models;
using ElancoLibrary.Models.Offers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElancoUI.Areas.Identity.Pages.Account.Manage
{
    public class RebateModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RebateModel> _logger;
        private readonly IRebateData _rebateData;
        private readonly IOfferData _offerData;

        public FormModel Rebate { get; set; }
        public OfferModel Offer { get; set; }

        public RebateModel(UserManager<IdentityUser> userManager, ILogger<RebateModel> logger, 
            IRebateData rebateData, IOfferData offerData)
        {
            _userManager = userManager;
            _logger = logger;
            _rebateData = rebateData;
            _offerData = offerData;
        }

        public async Task OnGet(string id)
        {
            try
            {
                string userId = _userManager.GetUserId(User);

                Rebate = await _rebateData.GetSubmissionDetails(id, userId);
                Offer = await _offerData.GetOfferById(Rebate.OfferId);
            }
            catch (Exception)
            {
                // TODO: Add error modal
            }
        }
    }
}
