using movie_manager.Models;

namespace movie_manager.Services
{
    public interface IUserService
    {
        Task<bool> AddUser(UserRequest newUser);
        Task<string> Authenticate(UserLogin userLogin);
        Task<bool> CheckIfUserExists(string username);
        Task<string> GenerateToken(string role);
        Task<IEnumerable<UserResponse>> GetAllUsers();
        Task<string> Login(UserLogin userLogin);
    }
}