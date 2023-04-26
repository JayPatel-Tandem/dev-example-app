using dev_example_app.Models;

namespace dev_example_app.Repository
{
    public interface IUserInterface
    {
        public Task<IEnumerable<UserModel>> GetAllUsers();

        public Task<UserModel> CreateUser(UserModel user);
    }
}
