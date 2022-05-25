using ElancoUI.Models;
using ElancoUI.Models.DbContextModels;

namespace ElancoUI.Helpers
{
    public interface IFormHelper
    {
        void FormatFields(FormModel form, Dictionary<string, string> fields);
        void FormatAccountDetails(Account account, FormModel form, FormInteractionModel formInteraction);
        ElancoLibrary.Models.FormModel FormatFormForSubmission(FormModel form, FormInteractionModel formInteraction, string userId);
    }
}
