

using Assessment_5.Data;
using Assessment_5.Entitites;
using Assessment_5.Requests;
using Assessment_5.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Assessment_5.Services
{
    public class UserServices : IUserInterface
    {
        private readonly AppDbContext _context;

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

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        }

        public Task<string> RegisterAnEventAsync(Guid id, Event events)
        {
            throw new NotImplementedException();
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
