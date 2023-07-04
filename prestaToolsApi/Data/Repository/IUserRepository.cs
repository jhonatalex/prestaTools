using prestaToolsApi.ModelsEntity;

namespace prestaToolsApi.Data.Repository
{
    public interface IUserRepository
    {


        Task<ApiResponseUser> LoginUser(string email, string pasword);
        Task<ApiResponseUser> InsertUser(User user);
        Task<ApiResponseListUser> GetAllUser(); 




        Task<User> GetByUserId(int id);
     
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(User user);
        
     
    }



}
