using prestaToolsApi.ModelsEntity;

namespace prestaToolsApi.Data.Repository
{
    public interface IUserRepository
    {
     
        Task<IEnumerable<User>> GetAllUser(); 
        Task<User> GetByUserId(int id);
        Task<User> LoginUser(string email, string pasword);
        Task<bool>InsertUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(User user);
        
     
    }



}
