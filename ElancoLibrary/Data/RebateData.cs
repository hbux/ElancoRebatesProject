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
        private readonly ISqlDataAccess _dataAccess;
        private readonly ILogger<RebateData> _logger;

        public RebateData(ISqlDataAccess dataAccess, ILogger<RebateData> logger)
        {
            _dataAccess = dataAccess;
            _logger = logger;
        }

        public async Task SubmitRebate(FormModel form, string userId)
        {
            if (form.UserId != userId)
            {
                throw new UnauthorizedAccessException("Submission user ID does not match current user. Submission cancelled.");
            }

            await _dataAccess.SaveData<FormModel>("dbo.spRebate_Insert", form, "ElancoData");

            _logger.LogDebug("Rebate with ID: {Id} inserted into database at {Time}", form.Id, DateTime.UtcNow);
        }

        public async Task<FormModel> GetSubmissionDetails(string submissionId, string userId)
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

            _logger.LogDebug("Retrieved submission details by ID: {Id} at {Time}", submissionId, DateTime.UtcNow);

            FormModel rebate = rebateSubmission.FirstOrDefault();

            if (rebate?.UserId != userId)
            {
                throw new UnauthorizedAccessException("Submission user ID does not match current user.");
            }

            return rebate;
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

        public async Task<List<FormModel>> GetAllSubmissions(string userId)
        {
            var p = new
            {
                UserId = userId
            };

            List<FormModel> submittedRebates = await _dataAccess.LoadData<FormModel, dynamic>("dbo.spRebate_GetByUserId", p, "ElancoData");

            if (submittedRebates == null)
            {
                _logger.LogWarning("Failed to retrieve all submissions of user ID: {Id} from database at {Time}", userId, DateTime.UtcNow);
            }

            _logger.LogDebug("Retrieved {Count} submissions by user ID: {Id} at {Time}", submittedRebates.Count, userId ,DateTime.UtcNow);

            return submittedRebates;
        }
    }
}
