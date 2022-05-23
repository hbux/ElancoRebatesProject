using ElancoLibrary.DataAccess;
using ElancoLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Data
{
    public class RebateData : IRebateData
    {
        private ISqlDataAccess dataAccess;

        public RebateData(ISqlDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public async Task SubmitRebate(FormModel form)
        {
            // Then post data to the database
            await dataAccess.SaveData<FormModel>("dbo.spRebate_Insert", form, "ElancoData");
        }
    }
}
