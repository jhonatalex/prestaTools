using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using prestaToolsApi.ModelsEntity;

namespace prestaToolsApi.Data.Repository
{
    public class LenderRepository : ILenderRepository
    {
        private readonly PrestatoolsContext _context;

        //Declaración de variables para uso del ApiResponse
        string token = "tu_token";
        bool success;
        ErrorRes errorRes = new ErrorRes();
        string message;

        public LenderRepository(PrestatoolsContext context)
        {
            _context = context;
        }

        ////////////////////////////////////////////////////////////////
        ///               GET ALL LENDER
        ////////////////////////////////////////////////////////////////
        public async Task<ApiResponse<List<Lender>>> GetAllLender()
        {

            List<Lender> lenders = await _context.Lenders.ToListAsync();

            try
            {

                if (lenders.Count > 0)
                {
                    success = true;
                    message = "Lenders encontrados";
                }
                else
                {
                    success = true;
                    errorRes = new ErrorRes { /* establecer propiedades de ErrorRes */ };
                    message = "Lenders encontrados";
                }

                var response = new ApiResponse<List<Lender>>(lenders, token, success, errorRes, message);
                return response;

            }

            catch (Exception ex)
            {
                success = false;
                errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };
                message = "Error";

                var response = new ApiResponse<List<Lender>>(lenders, token, success, errorRes, message);
                return response;
            }
        }

        ////////////////////////////////////////////////////////////////
        ///               GET BY LENDER ID
        ////////////////////////////////////////////////////////////////
        
        public async Task<ApiResponse<Lender>> GetByLenderId(string identifier)
        {
            var lenderByEmail = await _context.Lenders.FirstOrDefaultAsync(u => u.Email == identifier);

            if (lenderByEmail == null)
            {
                success = false;
                message = "Lender no encontrado";
            }
            else
            {
                success = true;
                message = "Lender encontrado";
            }

            var response = new ApiResponse<Lender>(lenderByEmail, token, success, errorRes, message);
            return response;
        }

        ////////////////////////////////////////////////////////////////
        ///               INSERT LENDER
        ////////////////////////////////////////////////////////////////
        
        public async Task<ApiResponse<Lender>> InsertLender(Lender lender)
        {
            
            try
            {

                lender.DateUp = DateTime.Now.ToString("yyyy-MM-dd");

                _context.Lenders.Add(lender);
                int result = await _context.SaveChangesAsync();

                success = true;
                message = "Lender creado satisfactoriamente";

                lender = null;

                var response = new ApiResponse<Lender>(lender, token, success, errorRes, message);
                return response;

            }
            catch (Exception ex)
            {

                token = "n/a";
                success = false;
                errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };
                message = "Error al Insertar";

                lender = null;

                var response = new ApiResponse<Lender>(lender, token, success, errorRes, message);
                return response;
            }

        }

        ////////////////////////////////////////////////////////////////
        ///               LOGIN LENDER
        ////////////////////////////////////////////////////////////////
        
        public async Task<ApiResponse<Lender>> LoginLender(string email, string password)
        {

            try
            {

                Lender lender = await _context.Lenders.FirstOrDefaultAsync(u => u.Email == email);

                if (lender != null && VerifyPassword(password, lender.Password))
                {
                    success = true;
                    message = "¡Login exitoso!";

                }
                else
                {

                    success = false;
                    message = "Email o password incorrecto";
                    errorRes = new ErrorRes { /* establecer propiedades de ErrorRes */ };

                }

                //lender = null;

                var response = new ApiResponse<Lender>(lender, token, success, errorRes, message);
                return response;

            }
            catch (Exception ex)
            {
                Lender lender = null;
                success = false;
                message = "Error";
                var errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };

                var response = new ApiResponse<Lender>(lender, token, success, errorRes, message);
                return response;
            }

        }

        ////////////////////////////////////////////////////////////////
        ///               UPDATE LENDER
        ////////////////////////////////////////////////////////////////

        public async Task<ApiResponse<Lender>> UpdateLender(Lender lender)
        {
            string hashedPassword = HashPassword(lender.Password);

            lender.Password = hashedPassword;

            _context.Lenders.Update(lender);
            int result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                success = true;
                message = "Lender actualizado satisfactoriamente";
            }
            else
            {
                success = false;
                message = "Lender no encontrado";
            }

            lender = null;

            var response = new ApiResponse<Lender>(lender, token, success, errorRes, message);
            return response;

        }

        ////////////////////////////////////////////////////////////////
        ///               DELETE LENDER
        ////////////////////////////////////////////////////////////////

        public async Task<ApiResponse<string>> DeleteLender(Lender lender)
        {

            try
            {
                _context.Lenders.Remove(lender);
                int result = await _context.SaveChangesAsync();
                string email = lender.Email;

                if (result > 0)
                {
                    success = true;
                    message = "Lender borrado satisfactoriamente";
                }
                else
                {
                    token = "n/a";
                    success = false;
                    message = "Lender no encontrado";
                }

                var response = new ApiResponse<string>(email, token, success, errorRes, message);
                return response;
            }

            catch (Exception ex)
            {
                string email = lender.Email;
                token = "n/a";
                success = false;
                message = "Error: lender no encontrado";
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
