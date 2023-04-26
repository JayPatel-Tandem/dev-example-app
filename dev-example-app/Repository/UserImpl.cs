using Dapper;
using dev_example_app.DBContext;
using dev_example_app.Models;
using System.Data;

namespace dev_example_app.Repository
{
    public class UserImpl : IUserInterface
    {
        private readonly DapperContext _context;

        public UserImpl(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            var query = "SELECT * FROM UserTable";
            using (var connection = _context.CreateConnection())
            {
                var users = await connection.QueryAsync<UserModel>(query);
                return users.ToList();
            }
        }

        public async Task<UserModel> CreateUser(UserModel user)
        {
            //var query = "INSERT INTO UserTable (Id, Name, Age) VALUES (@Id ,@Name, @Age)";
            var query = "[dbo].[InsertUser]";

            var dynamicParameters = new DynamicParameters();

            dynamicParameters.Add("@Id", user.Id);
            dynamicParameters.Add("@Name", user.Name);
            dynamicParameters.Add("@Age", user.Age);

            var parameters = new
            {
                user.Id,
                user.Name,
                user.Age,
            };

            using (var connection = _context.CreateConnection())
            {
                var a = await connection.QueryAsync<object>(query, parameters, commandType: CommandType.StoredProcedure);

                return user;
            }


        }

    }
}
