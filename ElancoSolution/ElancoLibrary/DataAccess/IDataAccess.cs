using ElancoLibrary.Models.Rebates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.DataAccess
{
    public interface IDataAccess
    {
        List<IRebateOfferModel> LoadAllRebatesOffers();
    }
}
