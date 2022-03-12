using ElancoUI.Models;
using ElancoUI.Models.DbContextModels;

namespace ElancoUI.Helpers
{
    public class AccountHelper : IAccountHelper
    {
        public AccountHelper()
        {

        }

        public AccountModel FormatAccountDetails(Account dbAccount)
        {
            AccountModel account = new AccountModel();

            try
            {
                account.Id = dbAccount.Id;
                account.FirstName = dbAccount.FirstName;
                account.LastName = dbAccount.LastName;
                account.Address = DoesContainElements<Address>(dbAccount.Addresses) ? dbAccount.Addresses.Single().AddressLine1 : null;
                account.City = DoesContainElements<Address>(dbAccount.Addresses) ? dbAccount.Addresses.Single().City : null;
                account.State = DoesContainElements<Address>(dbAccount.Addresses) ? dbAccount.Addresses.Single().State : null;
                account.ZipCode = DoesContainElements<Address>(dbAccount.Addresses) ? dbAccount.Addresses.Single().ZipCode : null;
                account.Pets = dbAccount.Pets;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong with applying account details.");
            }

            return account;
        }

        private bool DoesContainElements<T>(List<T> list)
        {
            if (list.Count == 0)
            {
                return false;
            }

            return true;
        }
    }
}
