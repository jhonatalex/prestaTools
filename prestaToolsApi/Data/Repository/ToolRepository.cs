




//using Microsoft.EntityFrameworkCore;
//using prestaToolsApi.ModelsEntity;

//namespace prestaToolsApi.Data.Repository
//{
//    public class ToolRepository
//    {
//        private readonly PrestatoolsContext _context;

//        //Declaración de variables para uso del ApiResponse
//        string token = "tu_token";
//        bool success;
//        ErrorRes errorRes = new ErrorRes();
//        string message;

//        public ToolRepository(PrestatoolsContext context)
//        {
//            _context = context;
//        }

//        ////////////////////////////////////////////////////////////////
//        ///               GET ALL TOOLS
//        ////////////////////////////////////////////////////////////////

//        public async Task<ApiResponse<List<Tool>>> GetAllTool()
//        {

//            List<Tool> tools = await _context.Tools.ToListAsync();

//            try
//            {

//                if (tools.Count > 0)
//                {
//                    success = true;
//                    message = "Herramientas encontradas";
//                }
//                else
//                {
//                    success = true;
//                    errorRes = new ErrorRes { /* establecer propiedades de ErrorRes */ };
//                    message = "Herramientas encontradas";
//                }

//                var response = new ApiResponse<List<Tool>>(tools, token, success, errorRes, message);
//                return response;

//            }

//            catch (Exception ex)
//            {
//                success = false;
//                errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };
//                message = "Error";

//                var response = new ApiResponse<List<Tool>>(tools, token, success, errorRes, message);
//                return response;
//            }
//        }

//        ////////////////////////////////////////////////////////////////
//        ///               GET BY TOOL ID
//        ////////////////////////////////////////////////////////////////

//        public async Task<ApiResponse<Tool>> GetByToolId(int identifier)
//        {
//            var userById = await _context.Users.FirstOrDefaultAsync(u => u.Id == identifier);

//            if (userById == null)
//            {
//                success = false;
//                message = "Usuario no encontrado";
//            }
//            else
//            {
//                success = true;
//                message = "Usuario encontrado";
//            }

//            var response = new ApiResponse<Tool>(toolById, token, success, errorRes, message);
//            return response;
//        }

//        ////////////////////////////////////////////////////////////////
//        ///               INSERT TOOL
//        ////////////////////////////////////////////////////////////////

//        public async Task<ApiResponse<Tool>> InsertTool(Tool tool)
//        {

//            try
//            {

//                string hashedPassword = HashPassword(tool.Password);

//                user.Password = hashedPassword;
//                user.Date = DateTime.Now.ToString("yyyy-MM-dd");

//                _context.Users.Add(user);
//                int result = await _context.SaveChangesAsync();

//                success = true;
//                message = "Usuario creado satisfactoriamente";

//                user = null;

//                var response = new ApiResponse<Tool>(tool, token, success, errorRes, message);
//                return response;

//            }
//            catch (Exception ex)
//            {

//                token = "n/a";
//                success = false;
//                errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };
//                message = "Error al Insertar";

//                var response = new ApiResponse<Tool>(tool, token, success, errorRes, message);
//                return response;
//            }

//        }

//        ////////////////////////////////////////////////////////////////
//        ///               LOGIN TOOL
//        ////////////////////////////////////////////////////////////////

//        public async Task<ApiResponse<Tool>> LoginTool(string email, string password)
//        {

//            try
//            {

//                User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

//                if (user != null && VerifyPassword(password, user.Password))
//                {
//                    success = true;
//                    message = "¡Login exitoso!";

//                }
//                else
//                {

//                    success = false;
//                    message = "Email o password incorrecto";
//                    errorRes = new ErrorRes { /* establecer propiedades de ErrorRes */ };

//                }



//                var response = new ApiResponse<Tool>(null, token, success, errorRes, message);
//                return response;

//            }
//            catch (Exception ex)
//            {
//                Tool tool = null;
//                success = false;
//                message = "Error";
//                var errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };

//                var response = new ApiResponse<Tool>(null, token, success, errorRes, message);
//                return response;
//            }

//        }

//        ////////////////////////////////////////////////////////////////
//        ///               UPDATE TOOL
//        ////////////////////////////////////////////////////////////////

//        public async Task<ApiResponse<Tool>> UpdateTool(Tool tool)
//        {
//            //string hashedPassword = HashPassword(user.Password);

//            //user.Password = hashedPassword;

//            _context.Tools.Update(tool);
//            int result = await _context.SaveChangesAsync();

//            if (result > 0)
//            {
//                success = true;
//                message = "Usuario actualizado satisfactoriamente";
//            }
//            else
//            {
//                success = false;
//                message = "Usuario no encontrado";
//            }

//            //user = null;

//            var response = new ApiResponse<Tool>(null, token, success, errorRes, message);
//            return response;

//        }

//        ////////////////////////////////////////////////////////////////
//        ///               DELETE TOOL
//        ////////////////////////////////////////////////////////////////

//        public async Task<ApiResponse<string>> DeleteTool(Tool tool)
//        {

//            try
//            {
//                _context.Tools.Remove(tool);
//                int result = await _context.SaveChangesAsync();
//                //string email = user.Email;

//                if (result > 0)
//                {
//                    success = true;
//                    message = "Usuario borrado satisfactoriamente";
//                }
//                else
//                {
//                    token = "n/a";
//                    success = false;
//                    message = "Usuario no encontrado";
//                }

//                var response = new ApiResponse<string>(null, token, success, errorRes, message);
//                return response;
//            }

//            catch (Exception ex)
//            {
//                //string email = user.Email;
//                token = "n/a";
//                success = false;
//                message = "Error: usuario no encontrado";
//                var errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };
//                var response = new ApiResponse<string>(email, token, success, errorRes, message);
//                return response;
//            }

//        }

//        //METODOS ITERNOS

//        private string HashPassword(string password)
//        {
//            return BCrypt.Net.BCrypt.HashPassword(password);
//        }

//        private bool VerifyPassword(string password, string hashedPassword)
//        {
//            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
//        }

//    }

//}
//}
