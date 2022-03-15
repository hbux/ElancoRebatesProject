using ElancoUI.Models.DbContextModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ElancoUI.Data
{
    public class AccountData : IAccountData
    {
        private ApplicationDbContext _context;

        public AccountData(ApplicationDbContext context)
        {
            _context = context;
        }

        public Account GetAccountDetails(IdentityUser user)
        {
            Account account = _context.Account
                .Include(a => a.Addresses)
                .Include(a => a.Pets)
                .FirstOrDefault(a => a.User.Id == user.Id);

            if (account == null)
            {
                throw new Exception("Could not find account.");
            }

            return account;
        }

        public Pet GetPetById(int id)
        {
            Pet pet = _context.Pets.FirstOrDefault(p => p.Id == id);

            if (pet == null)
            {
                throw new Exception($"Could not find pet with ID: { id }.");
            }

            return pet;
        }

        public void SaveAccountDetails()
        {
            _context.SaveChanges();
        }
    }
}
