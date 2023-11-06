using Microsoft.AspNetCore.Mvc;
using proj_csharp_kiminoyume.DTOs.JobApplication;
using proj_csharp_kiminoyume.Helpers;
using proj_csharp_kiminoyume.Responses;
using proj_csharp_kiminoyume.Services.JobApplication;

namespace proj_csharp_kiminoyume.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JobApplicationController: ControllerBase
    {
        private readonly IJobApplicationBusinessLogic _businessLogic;

        public JobApplicationController(IJobApplicationBusinessLogic businessLogic)
        {
            _businessLogic = businessLogic;
        }

        /// <summary>
        /// Returns a list of job application.
        /// </summary>
        /// <returns>Returns JobApplicationListResponse</returns>
        /// <response code="200">Returns the job application list.</response>
        /// <response code="500">If there is an error processing the request.</response> 
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpGet]
        public async Task<ActionResult<JobApplicationListResponse>> GetJobApplicationList()
        {
            var response = new JobApplicationListResponse();

            try
            {
                var list = await _businessLogic.GetJobApplicationList();
                if (list.Count > 0)
                {
                    response.JobApplications = ConvertModelToDTO.ConvertJobApplicationListModelToDTO(list);
                    response.IsSuccess = true;
                }

                return Ok(response);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Creates new job application.
        /// </summary>
        /// <returns>Returns JobApplicationResponse</returns>
        /// <response code="201">Returns the new job application.</response>
        /// <response code="500">If there is an error processing the request.</response> 
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost]
        public async Task<ActionResult<JobApplicationResponse>> CreateNewJobApplication([FromBody]JobApplicationDTO request)
        {
            var response = new JobApplicationResponse();

            try
            {
                var modelRequest = ConvertDTOToModel.ConvertJobDTOToModel(request);
                if (modelRequest != null)
                {
                    var result = await _businessLogic.CreateNewJobApplication(modelRequest);
                    if (result != null)
                    {
                        response.JobApplication = ConvertModelToDTO.ConvertJobModelToDTO(result);
                        response.IsSuccess = true;
                    }
                }

                return Ok(response);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Updates job application.
        /// </summary>
        /// <returns>Returns JobApplicationResponse</returns>
        /// <response code="201">Returns the updated job application.</response>
        /// <response code="500">If there is an error processing the request.</response> 
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost]
        public async Task<ActionResult<JobApplicationResponse>> UpdateJobApplication([FromBody] JobApplicationDTO request)
        {
            var response = new JobApplicationResponse();

            try
            {
                var modelRequest = ConvertDTOToModel.ConvertJobDTOToModel(request);
                if (modelRequest != null)
                {
                    var result = await _businessLogic.UpdateJobApplication(modelRequest);
                    if (result != null)
                    {
                        response.JobApplication = ConvertModelToDTO.ConvertJobModelToDTO(result);
                        response.IsSuccess = true;
                    }
                }

                return Ok(response);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Performs soft delete on Job Application entry.
        /// </summary>
        /// <returns>Returns the status of the deletion.</returns>
        /// <param name="id">Delete Job Application Request</param>
        /// <response code="200">Returns IsSuccess</response>
        /// <response code="500">If there is an error processing the request.</response> 
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<BaseResponse>> DeleteJobApplication([FromBody]int id)
        {
            var response = new BaseResponse();

            try
            {
                await _businessLogic.DeleteJobApplication(id);
                response.IsSuccess = true;

                return Ok(response);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
