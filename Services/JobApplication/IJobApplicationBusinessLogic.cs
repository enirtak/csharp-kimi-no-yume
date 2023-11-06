using proj_csharp_kiminoyume.DTOs.JobApplication;

namespace proj_csharp_kiminoyume.Services.JobApplication
{
    public interface IJobApplicationBusinessLogic
    {
        Task<List<Models.JobApplication.JobApplication>> GetJobApplicationList();
        Task<Models.JobApplication.JobApplication?> CreateNewJobApplication(Models.JobApplication.JobApplication request);
        Task<Models.JobApplication.JobApplication?> UpdateJobApplication(Models.JobApplication.JobApplication request);
        Task DeleteJobApplication(int id);
    }
}
