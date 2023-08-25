using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using proj_csharp_kiminoyume.Data;
using proj_csharp_kiminoyume.Requests;
using proj_csharp_kiminoyume.Responses;
using proj_csharp_kiminoyume.Services.Auth;

namespace proj_csharp_kiminoyume.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AuthDBContext _context;
        private readonly TokenService _tokenService;

        public AuthenticateController(UserManager<IdentityUser> userManager, AuthDBContext context, TokenService tokenService)
        {
            _userManager = userManager;
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserRequest request)
        {
            if (!ModelState.IsValid || request == null) return BadRequest(ModelState);

            var createUserManager = await _userManager.CreateAsync(
                new IdentityUser
                {
                    UserName = request.UserName,
                    Email = request.Email
                }, 
                request.Password);

            if (!createUserManager.Succeeded)
            {
                foreach (var error in createUserManager.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            request.Password = string.Empty;
            return CreatedAtAction(nameof(RegisterUser), new { email = request.Email }, request);
        }

        [HttpPost]
        public async Task<ActionResult<AuthenticateUserResponse>> AuthenticateUser([FromBody] AuthenticateUserRequest request)
        {
            try
            {
                if (!ModelState.IsValid || request == null) return BadRequest(ModelState);

                var managedUser = await _userManager.FindByNameAsync(request.Username);
                if (managedUser == null) return BadRequest("Invalid Credentials");

                var isValidPassword = await _userManager.CheckPasswordAsync(managedUser, request.Password);
                if (!isValidPassword) return BadRequest("Invalid Credentials");

                var user = _context.Users.Where(x => x.UserName == managedUser.UserName).FirstOrDefault();
                if (user == null) return Unauthorized();

                var accessToken = _tokenService.CreateToken(user);
                await _context.SaveChangesAsync();

                return Ok(new AuthenticateUserResponse
                {
                    Email = user.Email,
                    Username = user.Email,
                    Token = accessToken
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
