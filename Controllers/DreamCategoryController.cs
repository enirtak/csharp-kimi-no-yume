using Microsoft.AspNetCore.Mvc;
using proj_csharp_kiminoyume.Helpers;
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
            var response = new CategoryResponse();

            try
            {
                response.Categories = await _businessLogic.GetCategoriesList();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message.ToString();
            }

            return response;
        }

        [HttpPost]
        public async Task<CategoryItemResponse> CreateNewCategory([FromBody] CategoryRequest request)
        {
            var response = new CategoryItemResponse();
            if (request == null) return response;

            try
            {
                var model = ConvertDTOToModel.ConvertCategoryDTOToModel(request.Category);
                if (model != null)
                {
                    var result = await _businessLogic.CreateCategory(model);
                    if (result != null)
                    {
                        response.Category = ConvertModelToDTO.ConvertCategoryModelToDTO(result);
                        response.IsSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message.ToString();
            }

            return response;
        }

        [HttpPost]
        public async Task<CategoryItemResponse> UpdateCategory([FromBody] CategoryRequest request)
        {
            var response = new CategoryItemResponse();
            if (request == null) return response;

            try
            {
                var model = ConvertDTOToModel.ConvertCategoryDTOToModel(request.Category);
                if (model != null)
                {
                    var result = await _businessLogic.UpdateCategory(model);
                    if (result != null)
                    {
                        response.Category = ConvertModelToDTO.ConvertCategoryModelToDTO(result);
                        response.IsSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message.ToString();
            }

            return response;
        }

        [HttpPost]
        public async Task<CategoryItemResponse> DeleteCategory([FromBody] CategoryIdRequest category)
        {
            var response = new CategoryItemResponse();
            if (category != null && category.Id == default) return response;

            try
            {
                await _businessLogic.DeleteCategory(category!.Id);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message.ToString();
                response.IsSuccess = false;
            }

            return response;
        }
    }
}
