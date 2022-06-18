using ElancoUI.Models.DbContextModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ElancoUI.Data
{
    public class AccountData : IAccountData
    {
        private ApplicationDbContext _context;
        private ILogger<AccountData> _logger;

        public AccountData(ApplicationDbContext context, ILogger<AccountData> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Account GetAccountDetails(IdentityUser user)
        {
            Account account = _context.Account
                .Include(a => a.Addresses)
                .Include(a => a.Pets)
                .FirstOrDefault(a => a.User.Id == user.Id);

            if (account == null)
            {
                _logger.LogWarning("Failed to retrieve account details of user ID: {Id} at {Time}", user.Id, DateTime.UtcNow);
                throw new Exception("Could not find account.");
            }

            _logger.LogDebug("Retrieved account details of user ID: {Id} at {Time}", user.Id, DateTime.UtcNow);

            return account;
        }

        public Account GetAccountDetailsByEmail(string email)
        {
            Account account = _context.Account
                .Include(a => a.Addresses)
                .Include(a => a.Pets)
                .Include(a => a.User)
                .FirstOrDefault(a => a.User.Email == email);

            if (account == null)
            {
                _logger.LogWarning("Failed to retrieve account details of user by email at {Time}", DateTime.UtcNow);
                throw new Exception("Could not find account.");
            }

            _logger.LogDebug("Retrieved account details of user by email at {Time}", DateTime.UtcNow);

            return account;
        }

        public Pet GetPetById(int id)
        {
            Pet pet = _context.Pets.FirstOrDefault(p => p.Id == id);

            if (pet == null)
            {
                _logger.LogWarning("Failed to retrieve pet details by pet ID: {Id} at {Time}", id, DateTime.UtcNow);
                throw new Exception($"Could not find pet with ID: { id }.");
            }

            _logger.LogDebug("Retrieved pet details by pet ID: {Id} at {Time}", id, DateTime.UtcNow);

            return pet;
        }

        public Pet GetPetByIdNotracking(int id)
        {
            Pet pet = _context.Pets.AsNoTracking().FirstOrDefault(p => p.Id == id);

            if (pet == null)
            {
                _logger.LogWarning("Failed to retrieve pet details (no tracking) by pet ID: {Id} at {Time}", id, DateTime.UtcNow);
                throw new Exception($"Could not find pet with ID: { id }.");
            }

            _logger.LogDebug("Retrieved pet details (no tracking) by pet ID: {Id} at {Time}", id, DateTime.UtcNow);

            return pet;
        }

        public Address GetAddressById(int id)
        {
            Address address = _context.Addresses.FirstOrDefault(a => a.Id == id);

            if (address == null)
            {
                _logger.LogWarning("Failed to retrieve address details by address ID: {Id} at {Time}", id, DateTime.UtcNow);
                throw new Exception($"Could not find address with ID: { id }.");
            }

            _logger.LogDebug("Retrieved address details by address ID: {Id} at {Time}", id, DateTime.UtcNow);

            return address;
        }

        public Address GetAddressByIdNotracking(int id)
        {
            Address address = _context.Addresses.AsNoTracking().FirstOrDefault(a => a.Id == id);

            if (address == null)
            {
                _logger.LogWarning("Failed to retrieve address details (no tracking) by address ID: {Id} at {Time}", id, DateTime.UtcNow);
                throw new Exception($"Could not find address with ID: { id }.");
            }

            _logger.LogDebug("Retrieved address details (no tracking) by address ID: {Id} at {Time}", id, DateTime.UtcNow);

            return address;
        }

        public void RemovePet(Pet pet)
        {
            _context.Pets.Remove(pet);

            _logger.LogDebug("Removed pet from database by pet ID: {Id} at {Time}", pet.Id, DateTime.UtcNow);
        }

        public void RemoveAddress(Address address)
        {
            _context.Addresses.Remove(address);

            _logger.LogDebug("Removed address from database by address ID: {Id} at {Time}", address.Id, DateTime.UtcNow);
        }

        public void SaveAccountDetails()
        {
            _context.SaveChanges();

            _logger.LogDebug("Account details saved at {Time}", DateTime.UtcNow);
        }
    }
}
