using ElancoLibrary.DataAccess;
using ElancoLibrary.Models.Rebates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Services
{
    public class RebateOfferService
    {
        private IDataAccess _dataAccess;
        private List<IRebateOfferModel> _rebateOffers;

        public RebateOfferService(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<IRebateOfferModel> GetRebateOffers()
        {
            return _rebateOffers;
        }

        public void FindRebateOffers(Dictionary<string, string> rawRebateDetails)
        {
            // This will find any matching rebates comparing it to data access
        }
    }
}
