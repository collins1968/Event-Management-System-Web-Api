using Assessment_5.Entitites;
using Assessment_5.Requests;

namespace Assessment_5.Services.Interface
{
    public interface IUserInterface
    {
      
        //Adding a User
        Task<string> AddUserAsync(User user);
        //update
        Task<string> UpdateUserAsync(User user);
        //delete
        Task<string> DeleteUserAsync(User user);
        //Get All User
        Task<List<User>> GetAllUsersAsync();
        //Get One User
        Task<User> GetUserByIdAsync(Guid id);
        //Register for an event
        Task<string>RegisterAnEventAsync(RegisterEvent registerEvent);



    }
}
