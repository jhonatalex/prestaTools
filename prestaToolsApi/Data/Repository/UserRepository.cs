using Dapper;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;

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

            var sql = @"INSERT INTO user(name, last_name, password, email, telephone, address, d_identidad, date_up, state)
                VALUES(@name, @last_name, @password, @email, @telephone, @address, @d_identidad, @date_up, @state)";

            string date = DateTime.UtcNow.ToString("MM-dd-yyyy");

            // Cifrar la contraseña antes de insertarla
            string hashedPassword = HashPassword(user.password);

            var result = await db.ExecuteAsync(sql, new
            {
                user.name,
                user.last_name,
                password = hashedPassword,
                user.email,
                user.telephone,
                user.address,
                user.d_identidad,
                user.date_up,
                user.state
            });

            return result > 0;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public async Task<User> LoginUser(string email, string password)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM user WHERE email = @Email";
            var result = await db.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });

            if (result != null && VerifyPassword(password, result.password))
            {
                return result;
            }

            return null;
        }



        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
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

      

        Task<bool> IUserRepository.InsertUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
