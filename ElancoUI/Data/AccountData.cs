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
                .Include(p => p.Pets)
                .Where(x => x.User.Id == user.Id)
                .ToList();

            if (account.Count == 0)
            {
                return new Account();
            }

            return account.Single();
        }
    }
}
