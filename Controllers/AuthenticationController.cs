using Assessment_5.Entitites;
using Assessment_5.Requests;
using Assessment_5.Responses;
using Assessment_5.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assessment_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserInterface _userInterface;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IUserInterface userInterface, IMapper mapper, IConfiguration configuration)
        {
            _userInterface = userInterface;
            _mapper = mapper;
            _configuration = configuration;
        }

        //login
        [HttpPost("login")]
        public async Task<ActionResult<SuccessMessage>> Login(LoginRequest loginRequest)
        {
            var result = await _userInterface.LoginAsync(loginRequest);
            return Ok($"Token: {result}");
        }

        //logout
        [HttpPost("logout")]
        public async Task<ActionResult<SuccessMessage>> Logout()
        {
            var result = await _userInterface.LogoutAsync();
            return Ok(result);
        }

        //reset password
        //[HttpPost("resetpassword")]
       


    }
}
