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

        public Pet GetPetByIdNotracking(int id)
        {
            Pet pet = _context.Pets.AsNoTracking().FirstOrDefault(p => p.Id == id);

            if (pet == null)
            {
                throw new Exception($"Could not find pet with ID: { id }.");
            }

            return pet;
        }

        public Address GetAddressById(int id)
        {
            Address address = _context.Addresses.FirstOrDefault(a => a.Id == id);

            if (address == null)
            {
                throw new Exception($"Could not find address with ID: { id }.");
            }

            return address;
        }

        public Address GetAddressByIdNotracking(int id)
        {
            Address address = _context.Addresses.AsNoTracking().FirstOrDefault(a => a.Id == id);

            if (address == null)
            {
                throw new Exception($"Could not find address with ID: { id }.");
            }

            return address;
        }

        public void RemovePet(Pet pet)
        {
            _context.Pets.Remove(pet);
        }

        public void RemoveAddress(Address address)
        {
            _context.Addresses.Remove(address);
        }

        public void SaveAccountDetails()
        {
            _context.SaveChanges();
        }
    }
}
