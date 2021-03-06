using ElancoUI.Models.DbContextModels;
using Microsoft.AspNetCore.Identity;

namespace ElancoUI.Data
{
    public interface IAccountData
    {
        void CreateAccount(IdentityUser user);
        Account GetAccountDetails(IdentityUser user);
        Account GetAccountDetailsByEmail(string email);
        Pet GetPetById(int id);
        Pet GetPetByIdNotracking(int id);
        Address GetAddressById(int id);
        Address GetAddressByIdNotracking(int id);
        void RemovePet(Pet pet);
        void RemoveAddress(Address Address);
        void SaveAccountDetails();
    }
}