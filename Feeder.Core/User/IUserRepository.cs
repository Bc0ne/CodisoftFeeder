namespace Feeder.Core.User
{
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(long userId);

        Task<User> GetUserByEmailAsync(string email);

        User AuthenticateAsync(User user, string password);

        Task<User> RegisterAsync(User user, string password);
    }
}
