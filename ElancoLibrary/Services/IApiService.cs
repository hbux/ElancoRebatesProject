using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Services
{
    public interface IApiService
    {
        Task<Dictionary<string, string>> AnalyseInvoice(string filePath);
        Task<List<string>> AnalyseProductImage(string filePath);
    }
}
