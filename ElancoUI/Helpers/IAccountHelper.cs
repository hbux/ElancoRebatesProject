using ElancoUI.Models;
using ElancoUI.Models.DbContextModels;

namespace ElancoUI.Helpers
{
    public interface IAccountHelper
    {
        AccountModel FormatAccountDetails(Account dbAccount);
    }
}