using ElancoUI.Data.Models;
using ElancoUI.Models;
using Microsoft.AspNetCore.Identity;

namespace ElancoUI.Data
{
    public interface IAccountData
    {
        Account GetAccountDetails(IdentityUser user);
        void SaveAccountDetails(AccountModel account);
    }
}