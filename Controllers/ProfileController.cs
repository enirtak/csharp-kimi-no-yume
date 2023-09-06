using Microsoft.AspNetCore.Mvc;
using proj_csharp_kiminoyume.Helpers;
using proj_csharp_kiminoyume.Requests;
using proj_csharp_kiminoyume.Responses;
using proj_csharp_kiminoyume.Services.Profile;

namespace proj_csharp_kiminoyume.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private IProfileBusinessLogic _profileBusinessLogic;

        public ProfileController(IProfileBusinessLogic profileBusinessLogic)
        {
            _profileBusinessLogic = profileBusinessLogic;
        }

        [HttpPost]
        public async Task<ProfileResponse?> CreateProfile(ProfileRequest request)
        {
            if (request is null || request?.Person is null) return null;
            var response = new ProfileResponse();

            try
            {
                var dtoRequest = ConvertDTOToModel.ConvertPersonDTOToModel(request.Person);
                var result = await _profileBusinessLogic.CreateNewProfile(dtoRequest);
                if (result is not null)
                {
                    response.IsSuccess = true;
                    response.Person = ConvertModelToDTO.ConvertPersonModelToDTO(result);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        [HttpGet]
        public async Task<ProfileListResponse?> GetProfileList(ProfileListRequest request)
        {
            if (request is null) return null;
            var response = new ProfileListResponse();

            try
            {
                var profileList = await _profileBusinessLogic.GetProfileList(request.IsGetAll);
                if (profileList is not null && profileList.Count > 0)
                {
                    var profilesDTO = ConvertModelToDTO.ConvertPersonModelToDTO(profileList);
                    response.Persons = profilesDTO;
                    response.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        [HttpGet]
        public async Task<ProfileResponse> GetCurrentProfile()
        {
            var response = new ProfileResponse();
            try
            {
                var result = await _profileBusinessLogic.GetProfile();
                if (result is not null)
                {
                    var profileDTO = ConvertModelToDTO.ConvertPersonModelToDTO(result);
                    response.Person = profileDTO;
                    response.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        [HttpPut]
        public async Task<ProfileResponse?> UpdateProfile(ProfileRequest request)
        {
            if (request is null || request?.Person is null) return null;
            var response = new ProfileResponse();

            try
            {
                var modelRequest = ConvertDTOToModel.ConvertPersonDTOToModel(request.Person);

                var result = await _profileBusinessLogic.UpdateProfile(modelRequest);
                if (result is not null)
                {
                    response.Person = ConvertModelToDTO.ConvertPersonModelToDTO(result);
                }

                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
    }
}
