using Microsoft.AspNetCore.Mvc;
using proj_csharp_kiminoyume.Helpers;
using proj_csharp_kiminoyume.Models;
using proj_csharp_kiminoyume.Requests;
using proj_csharp_kiminoyume.Responses;
using proj_csharp_kiminoyume.Services;
using static proj_csharp_kiminoyume.Responses.DreamCategoryResponse;
using static proj_csharp_kiminoyume.Responses.DreamDictionaryResponse;

namespace proj_csharp_kiminoyume.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class DreamCategoryController : ControllerBase
    {
        private readonly IEntityActionBusinessLogic<DreamCategory> _businessLogic;

        public DreamCategoryController(IEntityActionBusinessLogic<DreamCategory> businessLogic)
        {
            _businessLogic = businessLogic;
        }

        /// <summary>
        /// Creates a list of dream category.
        /// </summary>
        /// <returns>Returns a CategoryResponse</returns>
        /// <response code="200">Returns the list of dream category.</response>
        /// <response code="500">If there is an error processing the request.</response> 
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        //[AllowAnonymous]
        public async Task<ActionResult<CategoryResponse>> GetCategories()
        {
            var response = new CategoryResponse();

            try
            {
                var list = await _businessLogic.GetList();
                if (list == null) return response;

                var categoryList = ConvertModelToDTO.ConvertCategoryListModelToDTO(list);
                if (categoryList == null) return response;

                response.Categories = categoryList;
                response.IsSuccess = true;
                return Ok(response);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Creates a new dream category.
        /// </summary>
        /// <returns>Returns CategoryItemResponse</returns>
        /// <param name="request">Create New Dream Category Request</param>
        /// <response code="201">Returns the new dream category.</response>
        /// <response code="500">If there is an error processing the request.</response> 
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost]
        public async Task<ActionResult<CategoryItemResponse>> CreateNewCategory([FromBody] DreamCategoryRequest request)
        {
            var response = new CategoryItemResponse();
            if (request == null || request?.Category == null) return response;

            try
            {
                var categoryModel = ConvertDTOToModel.ConvertCategoryDTOToModel(request.Category);
                if (categoryModel == null) return response;

                var newCategory = await _businessLogic.Create(categoryModel);
                if (newCategory == null) return response;

                response.Category = ConvertModelToDTO.ConvertCategoryModelToDTO(newCategory);
                response.IsSuccess = true;
                return Created(nameof(CreateNewCategory), response);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Updates a dream category.
        /// </summary>
        /// <returns>Returns CategoryItemResponse</returns>
        /// <param name="request">Update Dream Category Request</param>
        /// <response code="200">Returns the updated dream category.</response>
        /// <response code="500">If there is an error processing the request.</response> 
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<CategoryItemResponse>> UpdateCategory([FromBody] DreamCategoryRequest request)
        {
            var response = new CategoryItemResponse();
            if (request == null || request?.Category == null) return response;

            try
            {
                var categoryModel = ConvertDTOToModel.ConvertCategoryDTOToModel(request.Category);
                if (categoryModel == null) return response;

                var updatedCategory = await _businessLogic.Update(categoryModel);
                if (updatedCategory == null) return response;

                response.Category = ConvertModelToDTO.ConvertCategoryModelToDTO(updatedCategory);
                response.IsSuccess = true;
                return Ok(response);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Performs soft delete on Dream Category entry.
        /// </summary>
        /// <returns>Returns the status of the deletion.</returns>
        /// <param name="request">Delete Dream Category Request</param>
        /// <response code="200">Returns IsSuccess</response>
        /// <response code="500">If there is an error processing the request.</response> 
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<ActionResult<BaseResponse>> DeleteCategory([FromBody] int id)
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
