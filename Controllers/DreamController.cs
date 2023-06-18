using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using proj_csharp_kiminoyume.BusinessLogics;
using proj_csharp_kiminoyume.Data;
using static proj_csharp_kiminoyume.Requests.DreamRequest;
using static proj_csharp_kiminoyume.Responses.DreamResponse;

namespace proj_csharp_kiminoyume.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DreamController : ControllerBase
    {
        private AppDBContext _context;
        private DreamBusinessLogic _businessLogic;

        public DreamController(AppDBContext context, IDistributedCache cache)
        {
            _context = context;
            _businessLogic = new DreamBusinessLogic(context, cache);
        }

        [HttpGet]
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
                var model = new Helpers.ConvertDTOToModel().ConvertDictionaryDTOToModel(request.DreamItem);
                if (model != null)
                {
                    await _businessLogic.CreateDream(model);
                }

                response.IsSuccess = true;
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
                var model = new Helpers.ConvertDTOToModel().ConvertDictionaryDTOToModel(request.DreamItem);
                if (model != null) await _businessLogic.UpdateDream(model);

                response.IsSuccess = true;
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
                var model = new Helpers.ConvertDTOToModel().ConvertDictionaryDTOToModel(request.DreamItem);
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
        public async Task<CategoryResponse> CreateNewCategory([FromBody] CategoryRequest request)
        {
            var response = new CategoryResponse();
            if (request == null) return response;

            try
            {
                var model = new Helpers.ConvertDTOToModel()?.ConvertCategoryDTOToModel(request.Category);
                if (model != null) await _businessLogic.CreateCategory(model);

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
        public async Task<CategoryResponse> UpdateCategory([FromBody] CategoryRequest request)
        {
            var response = new CategoryResponse();
            if (request == null) return response;

            try
            {
                var model = new Helpers.ConvertDTOToModel()?.ConvertCategoryDTOToModel(request.Category);
                if (model != null)
                {
                    await _businessLogic.UpdateCategoryById(model);
                    response.IsSuccess = true;
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
                var model = new Helpers.ConvertDTOToModel()?.ConvertCategoryDTOToModel(request.Category);
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
