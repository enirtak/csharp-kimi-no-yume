using Azure;
using Microsoft.AspNetCore.Mvc;
using proj_csharp_kiminoyume.BusinessLogics;
using proj_csharp_kiminoyume.Data;
using proj_csharp_kiminoyume.Helpers;
using proj_csharp_kiminoyume.Requests;
using proj_csharp_kiminoyume.Responses;

namespace proj_csharp_kiminoyume.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private AppDBContext _context;
        private ProfileBusinessLogic _profileBusinessLogic;

        public ProfileController(AppDBContext context)
        {
            _context = context;
            _profileBusinessLogic = new ProfileBusinessLogic(context);
        }

        [HttpPost]
        public async Task<ProfileResponse?> CreateProfile(ProfileRequest request)
        {
            var response = new ProfileResponse();

            try
            {
                //request.Person = _profileBusinessLogic.CreateDummyPersonRequest();

                var dtoRequest = ConvertDTOToModel.ConvertPersonDTOToModel(request.Person);
                var result = await _profileBusinessLogic.CreateNewProfile(dtoRequest);
                if (result != null)
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
        public async Task<ProfileListResponse> GetProfileList(ProfileListRequest request)
        {
            var response = new ProfileListResponse();
            try
            {
                var profileList = await _profileBusinessLogic.GetProfileList(request.IsGetAll);
                if (profileList != null && profileList.Count > 0)
                {
                    var profilesDTO = _profileBusinessLogic.ConvertPersonToDTO(profileList);
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
                var profile = await _profileBusinessLogic.GetCurrentProfile();
                if (profile != null)
                {
                    var profileDTO = ConvertModelToDTO.ConvertPersonModelToDTO(profile);
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
        public async Task<ProfileResponse> UpdateProfile(ProfileRequest request)
        {
            var response = new ProfileResponse();
            try
            {
                //request.Person = _profileBusinessLogic.CreateDummyPersonRequest()!;
                var modelRequest = ConvertDTOToModel.ConvertPersonDTOToModel(request.Person);

                var result = await _profileBusinessLogic.UpdateProfile(modelRequest);
                if (result != null)
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
