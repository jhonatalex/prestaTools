using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prestaToolsApi.ModelsEntity;
using System.Drawing.Text;

namespace prestaToolsApi.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly PrestatoolsContext _context;

        //Declaración de variables para uso del ApiResponse
        string token = "tu_token";
        bool success;
        ErrorRes errorRes = new ErrorRes();
        string message;

        public UserRepository(PrestatoolsContext context)
        {
            _context = context;
        }

        ////////////////////////////////////////////////////////////////
        ///               GET ALL USER
        ////////////////////////////////////////////////////////////////

        public async Task<ApiResponse<List<User>>> GetAllUser()
        {

            List<User> users = await _context.Users.ToListAsync();

            try
            {

                if (users.Count > 0)
                {
                    success = true;
                    message = "Usuarios encontrados";
                }
                else
                {
                    success = true;
                    errorRes = new ErrorRes { /* establecer propiedades de ErrorRes */ };
                    message = "Usuarios encontrados";
                }

                var response = new ApiResponse<List<User>>(users, token, success, errorRes, message);
                return response;

            }

            catch (Exception ex)
            {
                success = false;
                errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };
                message = "Error";

                var response = new ApiResponse<List<User>>(users, token, success, errorRes, message);
                return response;
            }
        }

        ////////////////////////////////////////////////////////////////
        ///               GET BY USER ID
        ////////////////////////////////////////////////////////////////

        public async Task<ApiResponse<User>> GetByUserId(int identifier)
        {
            var userById = await _context.Users.FirstOrDefaultAsync(u => u.Id == identifier);
            
            if (userById == null)
            {
                success = false;
                message = "Usuario no encontrado";
            }
            else
            {
                success = true;
                message = "Usuario encontrado";
            }

            var response = new ApiResponse<User>(userById, token, success, errorRes, message);
            return response;
        }

        ////////////////////////////////////////////////////////////////
        ///               INSERT USER
        ////////////////////////////////////////////////////////////////

        public async Task<ApiResponse<User>> InsertUser(User user)
        {
    
            try { 
            
                string hashedPassword = HashPassword(user.Password);

                user.Password = hashedPassword;
                user.Date = DateTime.Now.ToString("yyyy-MM-dd");

                _context.Users.Add(user);
                int result = await _context.SaveChangesAsync();

                success = true;
                message = "Usuario creado satisfactoriamente";

                user = null;

                var response = new ApiResponse<User>(user, token, success, errorRes, message);
                return response;

            }   
            catch (Exception ex)
            {

                token = "n/a";
                success = false;
                errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };
                message = "Error al Insertar";

                user = null;

                var response = new ApiResponse<User>(user, token, success, errorRes, message);
                return response; 
            }

        }

        ////////////////////////////////////////////////////////////////
        ///               LOGIN USER
        ////////////////////////////////////////////////////////////////

        public async Task<ApiResponse<User>> LoginUser(string email, string password)
        {

            try
            {
                
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user != null && VerifyPassword(password, user.Password))
                {
                    success = true;
                    message = "¡Login exitoso!";

                }else{

                    success = false;
                    message = "Email o password incorrecto";
                    errorRes = new ErrorRes { /* establecer propiedades de ErrorRes */ };

                }

                //user = null;

                var response = new ApiResponse<User>(user, token, success, errorRes, message);
                return response;

            }
            catch (Exception ex)
            {
                User user = null;
                success = false;
                message = "Error";
                var errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };

                var response = new ApiResponse<User>(user, token, success, errorRes, message);
                return response;
            }

        }

        ////////////////////////////////////////////////////////////////
        ///               UPDATE USER
        ////////////////////////////////////////////////////////////////

        public async Task<ApiResponse<User>> UpdateUser(User user)
        {
            string hashedPassword = HashPassword(user.Password);

            user.Password = hashedPassword;

            _context.Users.Update(user);
            int result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                success = true;
                message = "Usuario actualizado satisfactoriamente";
            }
            else
            {
                success = false;
                message = "Usuario no encontrado";
            }

            user = null;

            var response = new ApiResponse<User>(user, token, success, errorRes, message);
            return response;

        }

        ////////////////////////////////////////////////////////////////
        ///               DELETE USER
        ////////////////////////////////////////////////////////////////

        public async Task<ApiResponse<string>> DeleteUser(User user)
        {

            try
            {
                _context.Users.Remove(user);
                int result = await _context.SaveChangesAsync();
                string email = user.Email;

                if (result > 0)
                {
                    success = true;
                    message = "Usuario borrado satisfactoriamente";
                }
                else
                {
                    token = "n/a";
                    success = false;
                    message = "Usuario no encontrado";
                }

                var response = new ApiResponse<string>(email, token, success, errorRes, message);
                return response;
            }

            catch (Exception ex)
            {
                string email = user.Email;
                token = "n/a";
                success = false;
                message = "Error: usuario no encontrado";
                var errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };
                var response = new ApiResponse<string>(email, token, success, errorRes, message);
                return response;
            }

        }

        //METODOS ITERNOS

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
        

