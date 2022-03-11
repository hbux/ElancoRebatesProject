using ElancoUI.Data.Models;
using ElancoUI.Models;

namespace ElancoUI.Helpers
{
    public interface IAccountHelper
    {
        AccountModel FormatAccountDetails(Account acc);
    }
}