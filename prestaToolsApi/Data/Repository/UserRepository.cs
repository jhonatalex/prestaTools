

using Azure;
using Microsoft.EntityFrameworkCore;
using prestaToolsApi.ModelsEntity;
using System.Drawing.Text;

namespace prestaToolsApi.Data.Repository
{
    public class UserRepository : IUserRepository
    {

       

        private readonly PrestatoolsContext _context;

        public UserRepository(PrestatoolsContext context)
        {
            _context = context;
        }

        public async Task<ApiResponseUser> InsertUser(User user)
        {
           
            try { 
            
            
                string hashedPassword = HashPassword(user.Password);

                user.Password = hashedPassword;
                user.Date = DateTime.Now.ToString("yyyy-MM-dd");

                _context.Users.Add(user);
                int result = await _context.SaveChangesAsync();

                var response = new ApiResponseUser
                {
                    token = "tu_token",
                    success = true,
                    message = "Usuario Creado Satisfactoriamente"
                };


                return response;

            }   
            catch (Exception ex)
            {

                var errorRes = new ErrorRes { code= ex.GetHashCode() , message= ex.Message };


                var response = new ApiResponseUser
                {
                    token = "n/a",
                    success = false,
                    message = "Error al Insertar",
                    error= errorRes

                };
                return response; 
            }


        }

        public async Task<ApiResponseUser> LoginUser(string email, string password)
        {
          

            try
            {

                User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);



               ApiResponseUser response = new ApiResponseUser();



                if (user != null && VerifyPassword(password, user.Password))
                {
                    
                    response.token = "tu_token";
                    response.user = user;
                    response.success = true;
                    response.message = "Login Exitoso";
       

                }else{

                    var errorRes = new ErrorRes { /* establecer propiedades de ErrorRes */ };
                    response.token = "tu_token";
                    response.success = false;
                    response.error = errorRes;
                    response.message = "Email o password incorrecta";
                 

                }


                return response;

            }
            catch (Exception ex)
            {

                var errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };


                var response = new ApiResponseUser
                {
                    token = "tu_token",
                    success = false,
                    error = errorRes,
                    message = "Error"
                
                };
                return response;
            }

        
        }




        public async Task<ApiResponseListUser> GetAllUser()
        {

            List<User> users = await _context.Users.ToListAsync();


            ApiResponseListUser response = new ApiResponseListUser();


            try { 



                        if (users.Count>0)
                        {

                            response.token = "tu_token";
                            //response.user ;
                            response.success = true;
                            response.message = "Exito";
                            response.users = users;


                        }
                        else
                        {

                            var errorRes = new ErrorRes { /* establecer propiedades de ErrorRes */ };
                            response.token = "tu_token";
                            response.success = false;
                            response.error = errorRes;
                            response.message = "Sin registros para mostrar";


                        }


                        return response;

            }catch (Exception ex)
            {

                var errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };

                response.token = "tu_token";
                response.success = false;
                response.error = errorRes;
                response.message = "Error";

                    

                return response;
            }



  


}



        public async Task<bool> DeleteUser(User user)
        {
            _context.Users.Remove(user);
            int result = await _context.SaveChangesAsync();

            return result > 0;
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





        //METODO ITERNOS



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
        

