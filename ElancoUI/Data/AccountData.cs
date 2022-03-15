using ElancoUI.Models;
using ElancoUI.Models.DbContextModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ElancoUI.Data
{
    public class AccountData : IAccountData
    {
        private ApplicationDbContext _db;

        public AccountData(ApplicationDbContext db)
        {
            _db = db;
        }

        public Account GetAccountDetails(IdentityUser user)
        {
            var account = _db.Account
                .Include(a => a.Addresses)
                .Include(a => a.Pets)
                .Where(x => x.User.Id == user.Id)
                .ToList();

            if (account.Count == 0)
            {
                return new Account();
            }

            return account.Single();
        }

        public void SaveAccountDetails(AccountModel account)
        {
            Account dbAccount = _db.Account
                .Include(a => a.Addresses)
                .Include(a => a.Pets)
                .FirstOrDefault(a => a.Id == account.Id);

            if (dbAccount == null)
            {
                throw new Exception("Error finding account.");
            }

            _db.Addresses.RemoveRange(dbAccount.Addresses);

            dbAccount.FirstName = account.FirstName;
            dbAccount.LastName = account.LastName;
            dbAccount.Addresses.Add(new Address()
            {
                AddressLine1 = account.Address,
                City = account.City,
                State = account.State,
                ZipCode = account.ZipCode
            });
            dbAccount.Pets = account.Pets;

            _db.SaveChanges();
        }

        public Pet GetPetById(int id)
        {
            return _db.Pets.Where(pet => pet.Id == id).Single();
        }
    }
}
