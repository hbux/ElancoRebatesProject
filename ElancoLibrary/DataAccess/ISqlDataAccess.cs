using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.DataAccess
{
    public interface ISqlDataAccess
    {
        public Task<List<T>> LoadData<T, U>();
        public Task SaveData<T>();
    }
}
