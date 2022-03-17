using ElancoUI.Models.DbContextModels;
using Microsoft.AspNetCore.Identity;

namespace ElancoUI.Data
{
    public interface IAccountData
    {
        Account GetAccountDetails(IdentityUser user);
        Pet GetPetById(int id);
        Pet GetPetByIdNotracking(int id);
        Address GetAddressById(int id);
        Address GetAddressByIdNotracking(int id);
        void RemovePet(Pet pet);
        void RemoveAddress(Address Address);
        void SaveAccountDetails();
    }
}