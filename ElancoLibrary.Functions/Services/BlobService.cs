using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Functions.Services
{
    public class BlobService
    {
        [FunctionName("")]
        public void Run([BlobTrigger("imageanalysis/{name}", Connection = "StorageConnection")] Stream myBlob, string name)
        {

        }
    }
}
