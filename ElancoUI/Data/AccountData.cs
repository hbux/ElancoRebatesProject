using ElancoUI.Data.Models;
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

        public void SaveAccountDetails(Account account)
        {
            Account acc = _db.Account
                .Include(a => a.Addresses)
                .Include(a => a.Pets)
                .FirstOrDefault(a => a.Id == account.Id);

            if (acc == null)
            {
                throw new Exception("Error finding account.");
            }

            _db.Addresses.RemoveRange(acc.Addresses);
            _db.Pets.RemoveRange(acc.Pets);

            acc.FirstName = account.FirstName;
            acc.LastName = account.LastName;
            acc.Addresses = account.Addresses;
            acc.Pets = account.Pets;

            _db.SaveChanges();
        }
    }
}
