using ElancoLibrary.DataAccess;
using ElancoLibrary.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.Data
{
    public class RebateData : IRebateData
    {
        private ISqlDataAccess _dataAccess;
        private ILogger<RebateData> _logger;

        public RebateData(ISqlDataAccess dataAccess, ILogger<RebateData> logger)
        {
            _dataAccess = dataAccess;
            _logger = logger;
        }

        public async Task SubmitRebate(FormModel form)
        {
            await _dataAccess.SaveData<FormModel>("dbo.spRebate_Insert", form, "ElancoData");

            _logger.LogDebug("Rebate with ID: {Id} inserted into database at {Time}", form.Id, DateTime.UtcNow);
        }

        public async Task<FormModel> GetSubmissionDetails(string submissionId)
        {
            var p = new
            {
                SubmissionId = submissionId,
            };

            var rebateSubmission = await _dataAccess.LoadData<FormModel, dynamic>("dbo.spRebate_GetById", p, "ElancoData");

            if (rebateSubmission == null)
            {
                _logger.LogWarning("Failed to retrieve submission details of ID: {Id} from database at {Time}", submissionId, DateTime.UtcNow);
                throw new NullReferenceException($"Failed to load rebate submission by ID: { submissionId }");
            }

            return rebateSubmission.FirstOrDefault();
        }

        public async Task UpdateUserAccess(string submissionId)
        {
            var p = new
            {
                SubmissionId = submissionId,
            };

            await _dataAccess.SaveData<dynamic>("dbo.spRebate_UpdateById", p, "ElancoData");

            _logger.LogDebug("Updated database for user access of submission ID: {Id} at {Time}", submissionId, DateTime.UtcNow);
        }
    }
}
