using ProyectCS50.Models;

namespace ProyectCS50.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByIdAsync(int id);
        Task<bool> ValidateUserAsync(string username, string password);
        Task RegisterUserAsync(User user, string password);

    }
}
