using ElancoUI.Data.Models;
using ElancoUI.Models;

namespace ElancoUI.Helpers
{
    public class AccountHelper : IAccountHelper
    {
        public AccountHelper()
        {

        }

        public AccountModel FormatAccountDetails(Account acc)
        {
            AccountModel account = new AccountModel();

            try
            {
                account.FirstName = acc.FirstName;
                account.LastName = acc.LastName;
                account.Address = DoesContainElements<Address>(acc.Addresses) ? acc.Addresses.Single().AddressLine1 : null;
                account.City = DoesContainElements<Address>(acc.Addresses) ? acc.Addresses.Single().City : null;
                account.State = DoesContainElements<Address>(acc.Addresses) ? acc.Addresses.Single().State : null;
                account.ZipCode = DoesContainElements<Address>(acc.Addresses) ? acc.Addresses.Single().ZipCode : null;
                account.PetName = DoesContainElements<Pet>(acc.Pets) ? acc.Pets.Single().Name : null;
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
