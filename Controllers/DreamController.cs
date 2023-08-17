using Microsoft.AspNetCore.Mvc;
using proj_csharp_kiminoyume.BusinessLogics;
using proj_csharp_kiminoyume.Data;
using proj_csharp_kiminoyume.Helpers;
using proj_csharp_kiminoyume.Interfaces;
using static proj_csharp_kiminoyume.Requests.DreamRequest;
using static proj_csharp_kiminoyume.Responses.DreamResponse;

namespace proj_csharp_kiminoyume.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class DreamController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly DreamBusinessLogic _businessLogic;

        public DreamController(AppDBContext context, IRedisCacheService cache)
        {
            _context = context;
            _businessLogic = new DreamBusinessLogic(context, cache);
        }

        [HttpGet]
        //[AllowAnonymous]
        public async Task<DreamListResponse> GetDreamDictionary()
        {
            var response = new DreamListResponse();

            try
            {
                var list = await _businessLogic.GetDreamList();
                response.DictionaryList = list;
                response.IsSuccess = true;

                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message.ToString();
            }

            return response;
        }

        [HttpPost]
        public async Task<DreamItemResponse> CreateNewDream([FromBody] DreamDictionaryRequest request)
        {
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
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message.ToString();
            }

            return response;
        }

        [HttpPut]
        public async Task<DreamItemResponse> UpdateDream([FromBody] DreamDictionaryRequest request)
        {
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
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message.ToString();
            }

            return response;
        }

        [HttpDelete]
        public async Task<DreamItemResponse> DeleteDream([FromBody] DreamDictionaryRequest request)
        {
            var response = new DreamItemResponse();
            if (request == null) return response;

            try
            {
                var model = ConvertDTOToModel.ConvertDictionaryDTOToModel(request.DreamItem);
                if (model != null) await _businessLogic.DeleteDream(model);

                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message.ToString();
            }

            return response;
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

        [HttpDelete]
        public async Task<CategoryItemResponse> DeleteCategory([FromBody] CategoryRequest request)
        {
            var response = new CategoryItemResponse();
            if (request == null) return response;

            try
            {
                var model = ConvertDTOToModel.ConvertCategoryDTOToModel(request.Category);
                if (model != null) await _businessLogic.DeleteCategory(model);

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
