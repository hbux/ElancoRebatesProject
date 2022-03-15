using ElancoUI.Models.DbContextModels;
using Microsoft.AspNetCore.Identity;

namespace ElancoUI.Data
{
    public interface IAccountData
    {
        Account GetAccountDetails(IdentityUser user);
        Pet GetPetById(int id);
        void SaveAccountDetails();
    }
}