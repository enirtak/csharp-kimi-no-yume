using Microsoft.AspNetCore.Mvc;
using proj_csharp_kiminoyume.Helpers;
using proj_csharp_kiminoyume.Models;
using proj_csharp_kiminoyume.Requests;
using proj_csharp_kiminoyume.Responses;
using proj_csharp_kiminoyume.Services;
using static proj_csharp_kiminoyume.Responses.DreamDictionaryResponse;

namespace proj_csharp_kiminoyume.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class DreamDictionaryController : ControllerBase
    {
        private readonly IEntityActionBusinessLogic<DreamDictionary> _businessLogic;

        public DreamDictionaryController(IEntityActionBusinessLogic<DreamDictionary> businessLogic)
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
        public async Task<ActionResult<DictionaryResponse>> GetDreamDictionary()
        {
            var response = new DictionaryResponse();

            try
            {
                var result = await _businessLogic.GetList();
                if (result == null) return response;

                var list = ConvertModelToDTO.ConvertDreamListModelToDTO(result);
                if (list == null) return response;

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
        public async Task<ActionResult<DictionarytemResponse>> CreateNewDream([FromBody] DreamDictionaryRequest request)
        {
            var response = new DictionarytemResponse();
            if (request == null || request?.DreamItem == null) return response;

            try
            {
                var model = ConvertDTOToModel.ConvertDictionaryDTOToModel(request.DreamItem);
                if (model == null) return response;

                var result = await _businessLogic.Create(model);
                if (result == null) return response;

                response.DreamItem = ConvertModelToDTO.ConvertDictionaryModelToDTO(result);
                response.IsSuccess = true;
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
        public async Task<ActionResult<DictionarytemResponse>> UpdateDream([FromBody] DreamDictionaryRequest request)
        {
            var response = new DictionarytemResponse();
            if (request == null || request?.DreamItem == null) return response;

            try
            {
                var model = ConvertDTOToModel.ConvertDictionaryDTOToModel(request.DreamItem);
                if (model == null) return response;

                var result = await _businessLogic.Update(model);
                if (result == null) return response;

                response.DreamItem = ConvertModelToDTO.ConvertDictionaryModelToDTO(result);
                response.IsSuccess = true;
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
        [HttpPut]
        public async Task<ActionResult<BaseResponse>> DeleteDream([FromBody] int id)
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
