using ElancoLibrary.Models;

namespace ElancoLibrary.Data
{
    public interface IRebateData
    {
        Task SubmitRebate(FormModel form, string userId);
        Task<FormModel> GetSubmissionDetails(string submissionId, string userId);
        Task UpdateUserAccess(string submissionId);
        Task<List<FormModel>> GetAllSubmissions(string userId);
    }
}