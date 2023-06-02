using Dapper;
using MySql.Data.MySqlClient;
using MySqlConnector;
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

        protected MySqlConnector.MySqlConnection dbConnection()
        {
            return new MySqlConnector.MySqlConnection(_connectionString.ConnectionString);
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

        public async Task<bool> DeleteUser(User user)
        {
            var db = dbConnection();

            var sql = @"DELETE FROM user WHERE id = @id";

            var result = await db.ExecuteAsync(sql, new {Id =user.id});

            return result > 0;
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            var db = dbConnection();

            var sql = @"SELECT id, name, last_name, password, email, telephone, address, d_identidad, date_up, state
                        FROM user";

            return await db.QueryAsync<User>(sql, new { });

        }

        public async Task<User> GetByUserId(int identifier)
        {
            var db = dbConnection();

            var sql = @"SELECT id, name, last_name, password, email, telephone, address, d_identidad, date_up, state
                        FROM user
                        WHERE id = @id";

            return await db.QueryFirstOrDefaultAsync<User>(sql, new { id = identifier});

        }


        public async Task<bool> UpdateUser(User user)
        {
            var db = dbConnection();

            var sql = @"UPDATE user
                        SET name = @name,
                            last_name = @last_name,
                            password = @password,
                            email = @email,
                            telephone = @telephone,
                            address = @address,
                            d_identidad = @d_identidad,
                            date_up = @date_up,
                            state = @state
                        WHERE id = @id";

            string date = DateTime.UtcNow.ToString("MM-dd-yyyy");

            // Cifrar la contraseña antes de insertarla
            string hashedPassword = HashPassword(user.password);

            var result = await db.ExecuteAsync(sql, new
            {
                user.id,
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

    }
}
