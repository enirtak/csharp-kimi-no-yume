using proj_csharp_kiminoyume.DTOs.JobApplication;

namespace proj_csharp_kiminoyume.Services.JobAppCustomField
{
    public interface IJobAppCustomFieldBusinessLogic
    {
        Task<List<JobAppCustomFieldDTO>> GetJobAppCustomFieldList();
        Task<Models.JobApplication.JobAppCustomField?> CreateNewJobAppCustomField();
        Task<Models.JobApplication.JobAppCustomField?> UpdateJobAppCustomField();
        Task DeleteJobAppCustomField(int id);
    }
}
