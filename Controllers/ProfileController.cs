using Microsoft.AspNetCore.Mvc;
using proj_csharp_kiminoyume.Helpers;
using proj_csharp_kiminoyume.Models;
using proj_csharp_kiminoyume.Requests;
using proj_csharp_kiminoyume.Responses;
using proj_csharp_kiminoyume.Services;

namespace proj_csharp_kiminoyume.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private IEntityRetrievalBusinessLogic<Person> _businessLogic;

        public ProfileController(IEntityRetrievalBusinessLogic<Person> businessLogic)
        {
            _businessLogic = businessLogic;
        }

        /// <summary>
        /// Creates a new profile.
        /// </summary>
        /// <returns>Returns ProfileResponse</returns>
        /// <param name="request">Create New Profile Request</param>
        /// <response code="201">Returns the new profile.</response>
        /// <response code="500">If there is an error processing the request.</response> 
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost]
        public async Task<ActionResult<ProfileResponse>> CreateProfile(ProfileRequest request)
        {
            var response = new ProfileResponse();
            if (request == null || request?.Person == null) return response;

            try
            {
                var dtoRequest = ConvertDTOToModel.ConvertPersonDTOToModel(request.Person);
                if (dtoRequest == null) return response;

                var result = await _businessLogic.Create(dtoRequest);
                if (result == null) return response;

                response.IsSuccess = true;
                response.Person = ConvertModelToDTO.ConvertPersonModelToDTO(result);
                return Ok(response);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError); 
            }
        }

        /// <summary>
        /// Creates new profile.
        /// </summary>
        /// <returns>Returns a ProfileListResponse</returns>
        /// <response code="200">Returns the list of profiles.</response>
        /// <response code="500">If there is an error processing the request.</response> 
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<ProfileListResponse>> GetProfileList()
        {
            var response = new ProfileListResponse();

            try
            {
                var result = await _businessLogic.GetList();
                if (result == null) return response;

                var profileList = ConvertModelToDTO.ConvertPersonListModelToDTO(result);
                if (profileList == null) return response;

                response.Persons = profileList!;
                response.IsSuccess = true;
                return Ok(response);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Updates a profile.
        /// </summary>
        /// <returns>Returns a ProfileListResponse</returns>
        /// <response code="200">Returns the updated profile.</response>
        /// <response code="500">If there is an error processing the request.</response> 
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<ProfileResponse>> GetCurrentProfile()
        {
            var response = new ProfileResponse();

            try
            {
                var result = await _businessLogic.GetById(0);
                if (result == null) return response;

                var profileDTO = ConvertModelToDTO.ConvertPersonModelToDTO(result);
                if (profileDTO == null) return response;

                response.Person = profileDTO;
                response.IsSuccess = true;

                return Ok(response);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Updates a profile.
        /// </summary>
        /// <returns>Returns ProfileResponse</returns>
        /// <param name="request">Update Profile Request</param>
        /// <response code="200">Returns the updated profile category.</response>
        /// <response code="500">If there is an error processing the request.</response> 
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<ActionResult<ProfileResponse>> UpdateProfile(ProfileRequest request)
        {
            var response = new ProfileResponse();
            if (request == null || request?.Person == null) return response;

            try
            {
                var result = ConvertDTOToModel.ConvertPersonDTOToModel(request.Person);
                if (result == null) return response;

                var profile = await _businessLogic.Update(result);
                if (result == null) return response;

                response.Person = ConvertModelToDTO.ConvertPersonModelToDTO(result);
                response.IsSuccess = true;
                return Ok(response);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Performs soft delete on Profile entry.
        /// </summary>
        /// <returns>Returns the status of the deletion.</returns>
        /// <param name="request">Delete Dream Request</param>
        /// <response code="200">Returns IsSuccess</response>
        /// <response code="500">If there is an error processing the request.</response> 
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<ActionResult<BaseResponse>> DeleteProfile([FromBody] int id)
        {
            var response = new BaseResponse();
            if (id == default) return response;

            try
            {
                await _businessLogic.Delete(id);

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
