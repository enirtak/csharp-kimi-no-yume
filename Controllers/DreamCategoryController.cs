using Azure;
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

        [HttpGet]
        //[AllowAnonymous]
        public async Task<CategoryResponse> GetCategories()
        {
            try
            {
                var categoriesList = await _businessLogic.GetCategoriesList();

                return new CategoryResponse()
                {
                    Categories = categoriesList,
                    IsSuccess = true
                };
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<CategoryItemResponse?> CreateNewCategory([FromBody] CategoryRequest request)
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

                return response;
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<CategoryItemResponse?> UpdateCategory([FromBody] CategoryRequest request)
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

                return response;
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<BaseResponse?> DeleteCategory([FromBody] DreamIdRequest request)
        {
            if (request is null || request?.Id is null) return null;

            try
            {
                await _businessLogic.DeleteCategory(request.Id);

                return new BaseResponse()
                {
                    IsSuccess = true
                };
            }
            catch
            {
                throw;
            }
        }
    }
}
