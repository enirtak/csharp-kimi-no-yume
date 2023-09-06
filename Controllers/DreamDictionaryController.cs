using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proj_csharp_kiminoyume.Helpers;
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

        [HttpPost]
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

        [HttpPost]
        public async Task<DreamItemResponse> DeleteDream([FromBody] DreamIdRequest request)
        {
            var response = new DreamItemResponse();
            if (request == null) return response;

            try
            {
                await _businessLogic.DeleteDream(request.Id);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message.ToString();
            }

            return response;
        }
    }
}
