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
            await dataAccess.SaveData<FormModel>("dbo.spRebate_Insert", form, "ElancoData");
        }

        public async Task<FormModel> GetSubmissionDetails(string submissionId)
        {
            var p = new
            {
                SubmissionId = submissionId,
            };

            var rebateSubmission = await dataAccess.LoadData<FormModel, dynamic>("dbo.spRebate_GetById", p, "ElancoData");

            return rebateSubmission.FirstOrDefault();
        }

        public async Task UpdateUserAccess(string submissionId)
        {
            var p = new
            {
                SubmissionId = submissionId,
            };

            await dataAccess.SaveData<dynamic>("dbo.spRebate_UpdateById", p, "ElancoData");
        }
    }
}
