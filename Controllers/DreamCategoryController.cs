using Microsoft.AspNetCore.Mvc;
using proj_csharp_kiminoyume.Helpers;
using proj_csharp_kiminoyume.Responses;
using proj_csharp_kiminoyume.Services.DreamCategory;
using static proj_csharp_kiminoyume.Requests.DreamRequest;
using static proj_csharp_kiminoyume.Responses.DreamResponse;

namespace proj_csharp_kiminoyume.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class DreamCategoryController : ControllerBase
    {
        private readonly IDreamCategoryBusinessLogic _businessLogic;

        public DreamCategoryController(IDreamCategoryBusinessLogic businessLogic)
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
                var categoriesList = await _businessLogic.GetCategoriesList();
                response.Categories = categoriesList;
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
        public async Task<ActionResult<CategoryItemResponse>?> CreateNewCategory([FromBody] CategoryRequest request)
        {
            if (request is null || request?.Category is null) return null;
            var response = new CategoryItemResponse();

            try
            {
                var categoryModel = ConvertDTOToModel.ConvertCategoryDTOToModel(request.Category);
                if (categoryModel is not null)
                {
                    var newCategory = await _businessLogic.CreateCategory(categoryModel);
                    if (newCategory is not null)
                    {
                        response.Category = ConvertModelToDTO.ConvertCategoryModelToDTO(newCategory);
                        response.IsSuccess = true;
                    }
                }

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
        public async Task<ActionResult<CategoryItemResponse>?> UpdateCategory([FromBody] CategoryRequest request)
        {
            if (request is null || request?.Category is null) return null;
            var response = new CategoryItemResponse();

            try
            {
                var categoryModel = ConvertDTOToModel.ConvertCategoryDTOToModel(request.Category);
                if (categoryModel is not null)
                {
                    var updatedCategory = await _businessLogic.UpdateCategory(categoryModel);
                    if (updatedCategory is not null)
                    {
                        response.Category = ConvertModelToDTO.ConvertCategoryModelToDTO(updatedCategory);
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
        [HttpPost]
        public async Task<ActionResult<BaseResponse>?> DeleteCategory([FromBody] DreamIdRequest request)
        {
            if (request is null || request?.Id is null) return null;
            var response = new BaseResponse();

            try
            {
                await _businessLogic.DeleteCategory(request.Id);
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
