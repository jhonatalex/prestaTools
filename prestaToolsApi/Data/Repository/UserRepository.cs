using Dapper;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using prestaToolsApi.ModelsEntity;

namespace prestaToolsApi.Data.Repository
{
    public class UserRepository : IUserRepository
    {

       

        private readonly PrestatoolsContext _context;

        public UserRepository(PrestatoolsContext context)
        {
            _context = context;
        }

        public async Task<bool> InsertUser(User user)
        {
            string hashedPassword = HashPassword(user.Password);

            user.Password = hashedPassword;
            user.Date = "2020-01-12";

            _context.Users.Add(user);
            int result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<User> LoginUser(string email, string password)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null && VerifyPassword(password, user.Password))
            {
                return user;
            }

            return null;
        }

        public async Task<bool> DeleteUser(User user)
        {
            _context.Users.Remove(user);
            int result = await _context.SaveChangesAsync();

            return result > 0;
        }
        public async Task<IEnumerable<User>> GetAllUser()
        {
            return await _context.Users.ToListAsync();
        }


        public async Task<User> GetByUserId(int identifier)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == identifier);
        }

        public async Task<bool> UpdateUser(User user)
        {
            string hashedPassword = HashPassword(user.Password);

            user.Password = hashedPassword;

            _context.Users.Update(user);
            int result = await _context.SaveChangesAsync();

            return result > 0;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }


   


    }




}
        

