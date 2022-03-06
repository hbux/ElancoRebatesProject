using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        public async Task<List<T>> LoadData<T, U>()
        {
            throw new NotImplementedException();
        }

        public async Task SaveData<T>()
        {
            throw new NotImplementedException();
        }
    }
}
