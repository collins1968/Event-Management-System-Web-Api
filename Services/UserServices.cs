

using Assessment_5.Data;
using Assessment_5.Entitites;
using Assessment_5.Requests;
using Assessment_5.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assessment_5.Services
{
    public class UserServices : IUserInterface
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public UserServices(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public UserServices(AppDbContext context)
        {
            _context = context;
        }
        public async Task<string> AddUserAsync(User user)
        {
            _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return "User Added Successfully";
           
        }

        public async Task<string> ChangePasswordAsync(ChangePasswordRequest changePasswordRequest)
        {
            await _context.Users.FirstOrDefaultAsync(x => x.Email == changePasswordRequest.Email && x.Password == changePasswordRequest.Password);
            return "Password Changed Successfully";
        }

        public async Task<string> DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return "User Deleted Successfully";
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            return _context.Users.ToListAsync();
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<string> LoginAsync(LoginRequest loginRequest)
        {
            var user = await GetUserByEmailAsync(loginRequest.Email);
            if(user == null)
            {
                return "User does not exist";
            }
            var password = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password);
            if(!password)
            {
                return "Password is incorrect";
            }

            var token = CreateToken(user);
            return token;

        }
        //create token
        private string CreateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim("NameIdentifier",user.Id.ToString()),
                new Claim("Name",user.Name),
                new Claim("Email",user.Email),
                new Claim("Role", user.Role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("TokenSecurity:SecretKey")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                               _configuration.GetValue<string>("TokenSecurity:Issuer"),
                               _configuration.GetValue<string>("TokenSecurity:Audience"),
                               claims: claims,
                               expires: DateTime.Now.AddDays(30),
                               signingCredentials: creds);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public Task<string> LogoutAsync()
        {
           _context.Users.FirstOrDefaultAsync(x => x.Email == null && x.Password == null);
            return Task.FromResult("Logout Successful");
        }

        public async Task<string> RegisterAnEventAsync(RegisterEvent registerEvent)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == registerEvent.UserId);
            var events = await _context.Events.FirstOrDefaultAsync(x => x.Id == registerEvent.EventId);
            if(user == null || events == null)
            {
                return "User or Event does not exist";
            }
            //user.Events.Add(events);
            events.Users.Add(user);
            await _context.SaveChangesAsync();
            return "User Registered for Event Successfully";
        }

        public async Task<string> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return "User Updated Successfully";
        }
    }
}
