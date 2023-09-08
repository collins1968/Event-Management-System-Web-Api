using Assessment_5.Entitites;
using Assessment_5.Requests;
using Assessment_5.Responses;
using Assessment_5.Services;
using Assessment_5.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Assessment_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInterface _userInterface;
        private readonly IMapper _mapper;

        public UserController(IUserInterface userInterface, IMapper mapper)
        {
            _userInterface = userInterface;
            _mapper = mapper;
        }

        //add user
        [HttpPost]
        public async Task<ActionResult<SuccessMessage>> AddUser(AddUser newUser)
        {
            var user = _mapper.Map<User>(newUser);
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            //user.Role = "Admin";
            var result = await _userInterface.AddUserAsync(user);
            var response = new SuccessMessage(200, "user added successfully");

            return CreatedAtAction(nameof(AddUser), response);
        }

        //get all users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUsers()
        {
            var response = await _userInterface.GetAllUsersAsync();
            var users = _mapper.Map<IEnumerable<UserResponse>>(response);
            return Ok(users);
        }

        //get user by id
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserByIdAsync(Guid id)
        {
            var result = await _userInterface.GetUserByIdAsync(id);
            if (result == null)
            {
                return NotFound(new SuccessMessage(404, "user Does Not Exist"));
            }
            return Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<SuccessMessage>> UpdateUserAsync(Guid id,AddUser updateUser)
        { 
            var response = await _userInterface.GetUserByIdAsync(id);
            if (response == null)
            {
                return NotFound(new SuccessMessage(404, "user Does Not Exist"));
            }
            var user = _mapper.Map(updateUser, response);
            var result = await _userInterface.UpdateUserAsync(user);
            return Ok(new SuccessMessage(200, result));
        }

        //delete user
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<SuccessMessage>> DeleteUserAsync(Guid id)
        {
            var response = await _userInterface.GetUserByIdAsync(id);

            if (response == null)
            {
                return NotFound(new SuccessMessage(404, "user Does Not Exist"));
            }
            var result = await _userInterface.DeleteUserAsync(response);
            return Ok(new SuccessMessage(200, result));
        }

        //register for an event
        [HttpPut("RegisterEvent")]
        public async Task<ActionResult<SuccessMessage>> buyCourse(RegisterEvent register)
        {
            try
            {
                var res = await _userInterface.RegisterAnEventAsync(register);
                return Ok(new SuccessMessage(204, res));
            }
            catch (Exception ex)
            {
                return BadRequest(new SuccessMessage(400, ex.Message));
            }

        }
    }
}
