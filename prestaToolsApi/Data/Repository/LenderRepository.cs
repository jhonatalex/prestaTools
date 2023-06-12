using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using prestaToolsApi.model;

namespace prestaToolsApi.Data.Repository
{
    public class LenderRepository : ILenderRepository
    {
        private readonly MySQLConfiguration _connectionString;

        public LenderRepository(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> InsertLender(Lender lender)
        {
            var db = dbConnection();

            var sql = @"INSERT INTO lender(d_identidad, name, last_name, password, email, telephone, address, number_bank, balance_wallet, date_up, state)
                VALUES(@d_identidad, @name, @last_name, @password, @email, @telephone, @address, @number_bank, @balance_wallet, @date_up, @state)";

            string date = DateTime.UtcNow.ToString("MM-dd-yyyy");

            // Cifrar la contraseña antes de insertarla
            string hashedPassword = HashPassword(lender.password);

            var result = await db.ExecuteAsync(sql, new
            {
                lender.id,
                lender.d_identidad,
                lender.name,
                lender.last_name,
                password = hashedPassword,
                lender.email,
                lender.telephone,
                lender.address,
                lender.number_bank,
                lender.balance_wallet,
                lender.date_up,
                lender.state
            });
      
            return result > 0;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public async Task<Lender> LoginLender(string email, string password)
        {
            var db = dbConnection();
            var sql = @"SELECT * FROM lender WHERE email = @Email";
            var result = await db.QueryFirstOrDefaultAsync<Lender>(sql, new { Email = email });

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

        public async Task<bool> DeleteLender(Lender lender)
        {
            var db = dbConnection();

            var sql = @"DELETE FROM lender WHERE id = @id";

            var result = await db.ExecuteAsync(sql, new { Id = lender.id });

            return result > 0;
        }

        public async Task<IEnumerable<Lender>> GetAllLenders()
        {
            var db = dbConnection();

            var sql = @"SELECT id, d_identidad, name, last_name, password, email, telephone, address, number_bank, balance_wallet, date_up, state
                        FROM lender";

            return await db.QueryAsync<Lender>(sql, new { });

        }

        public async Task<Lender> GetByLenderId(int identifier)
        {
            var db = dbConnection();

            var sql = @"SELECT id, d_identidad, name, last_name, password, email, telephone, address, number_bank, d_identidad, balance_wallet, date_up, state
                        FROM lender
                        WHERE id = @id";

            return await db.QueryFirstOrDefaultAsync<Lender>(sql, new { id = identifier });

        }

        public async Task<bool> UpdateLender(Lender lender)
        {
            var db = dbConnection();

            var sql = @"UPDATE lender   
                        SET d_identidad = @d_identidad,
                        name = @name,
                        last_name = @last_name,
                        password = @password,
                        email = @email,
                        telephone = @telephone,
                        address = @address,
                        number_bank = @number_bank,
                        balance_wallet = @balance_wallet,
                        date_up = @date_up,
                        state = @state
                      WHERE id = @id";

            string date = DateTime.UtcNow.ToString("MM-dd-yyyy");

            // Cifrar la contraseña antes de insertarla
            string hashedPassword = HashPassword(lender.password);

            var result = await db.ExecuteAsync(sql, new
            {
                lender.id,
                lender.d_identidad,
                lender.name,
                lender.last_name,
                password = hashedPassword,
                lender.email,
                lender.telephone,
                lender.address,
                lender.number_bank,
                lender.balance_wallet,
                lender.date_up,
                lender.state
            });

            return result > 0;
        }
    }
}
