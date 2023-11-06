using proj_csharp_kiminoyume.DTOs.JobApplication;

namespace proj_csharp_kiminoyume.Responses
{
    public class JobApplicationListResponse: BaseResponse
    {
        public List<JobApplicationDTO>? JobApplications { get; set; }
    }

    public class JobApplicationResponse : BaseResponse
    {
        public JobApplicationDTO? JobApplication { get; set; }
    }
}
