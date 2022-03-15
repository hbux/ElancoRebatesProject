using ElancoUI.Models.DbContextModels;
using Microsoft.AspNetCore.Identity;

namespace ElancoUI.Data
{
    public interface IAccountData
    {
        Account GetAccountDetails(IdentityUser user);
        Pet GetPetById(int id);
        Pet GetPetByIdNotracking(int id);
        void RemovePet(Pet pet);
        void SaveAccountDetails();
    }
}