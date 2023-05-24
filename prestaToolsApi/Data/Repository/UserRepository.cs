using Dapper;
using MySql.Data.MySqlClient;

namespace prestaToolsApi.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLConfiguration _connectionString;

        public UserRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> InsertUser(User user)
        {
            var db = dbConnection();

            var sql = @" INSERT INTO user(name, last_name, password, email, telephone, address, d_identidad, date, state)
                        VALUES(@name, @last_name, @password, @email, @telephone, @address, @d_identidad, @date, @state)";

            var result = await db.ExecuteAsync (sql, new 
                {user.name, user.last_name, user.password, user.email, user.telephone, user.address, user.d_identidad, user.date, user.state});

            return result > 0;
        }



        Task<bool> IUserRepository.DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<User>> IUserRepository.GetAllUser()
        {
            throw new NotImplementedException();
        }

        Task<User> IUserRepository.GetByUserId(int id)
        {
            throw new NotImplementedException();
        }


        Task<bool> IUserRepository.UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
