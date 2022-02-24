using ElancoLibrary.Models.Rebates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.DataAccess
{
    public class FileDataAccess : IDataAccess
    {
        private FileInfo file;

        public FileDataAccess()
        {
            file = new FileInfo("rebateOffers.txt");
        }

        public List<IRebateOfferModel> LoadAllRebatesOffers()
        {
            List<IRebateOfferModel> rebates = new List<IRebateOfferModel>();

            return rebates;
        }
    }
}
