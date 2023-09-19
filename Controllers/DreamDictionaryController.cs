using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using proj_csharp_kiminoyume.Helpers;
using proj_csharp_kiminoyume.Responses;
using proj_csharp_kiminoyume.Services.DreamDictionary;
using static proj_csharp_kiminoyume.Requests.DreamRequest;
using static proj_csharp_kiminoyume.Responses.DreamResponse;

namespace proj_csharp_kiminoyume.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class DreamDictionaryController : ControllerBase
    {
        private readonly IDreamDictionaryBusinessLogic _businessLogic;

        public DreamDictionaryController(IDreamDictionaryBusinessLogic businessLogic)
        {
            _businessLogic = businessLogic;
        }

        /// <summary>
        /// Returns a list of dictionary of dreams.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns the list.</response>
        /// <response code="500">If there is an error processing the request.</response>
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        //[AllowAnonymous]
        public async Task<ActionResult<DreamListResponse>> GetDreamDictionary()
        {
            var response = new DreamListResponse();

            try
            {
                var list = await _businessLogic.GetDreamList();
                response.DictionaryList = list;
                response.IsSuccess = true;

                return Ok(response);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Creates a new dream dictionary.
        /// </summary>
        /// <returns>Returns DreamItemResponse</returns>
        /// <param name="request">Create New Dream Request</param>
        /// <response code="201">Returns the new dream.</response>
        /// <response code="500">If there is an error processing the request.</response> 
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<DreamItemResponse>?> CreateNewDream([FromBody] DreamDictionaryRequest request)
        {
            if (request is null || request?.DreamItem is null) return null;
            var response = new DreamItemResponse();

            try
            {
                var model = ConvertDTOToModel.ConvertDictionaryDTOToModel(request.DreamItem);
                if (model != null)
                {
                    var result = await _businessLogic.CreateDream(model);
                    if (result != null)
                    {
                        response.DreamItem = ConvertModelToDTO.ConvertDictionaryModelToDTO(result);
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
        /// Updates a dream dictionary.
        /// </summary>
        /// <returns>Returns DreamItemResponse</returns>
        /// <param name="request">Update Dream Request</param>
        /// <response code="200">Returns the updated dream.</response>
        /// <response code="500">If there is an error processing the request.</response> 
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<DreamItemResponse>?> UpdateDream([FromBody] DreamDictionaryRequest request)
        {
            if (request is null || request?.DreamItem is null) return null;
            var response = new DreamItemResponse();

            try
            {
                var model = ConvertDTOToModel.ConvertDictionaryDTOToModel(request.DreamItem);
                if (model != null)
                {
                    var result = await _businessLogic.UpdateDream(model);
                    if (result != null)
                    {
                        response.DreamItem = ConvertModelToDTO.ConvertDictionaryModelToDTO(result);
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
        /// Performs soft delete on Dream Dictionary entry.
        /// </summary>
        /// <returns>Returns the status of the deletion.</returns>
        /// <param name="request">Delete Dream Request</param>
        /// <response code="200">Returns IsSuccess</response>
        /// <response code="500">If there is an error processing the request.</response> 
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<BaseResponse>?> DeleteDream([FromBody] DreamIdRequest request)
        {
            if (request == null) return null;
            var response = new BaseResponse();

            try
            {
                await _businessLogic.DeleteDream(request.Id);
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
