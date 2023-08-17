using prestaToolsApi.ModelsEntity;

namespace prestaToolsApi.Data.Repository
{
    public interface IUserRepository
    {
        Task<ApiResponse<List<User>>> GetAllUser();
        Task<ApiResponse<User>> GetByUserId(string email);
        Task<ApiResponse<User>> InsertUser(User user);
        Task<ApiResponse<User>> LoginUser(string email, string password);
        Task<ApiResponse<User>> UpdateUser(User user);
        Task<ApiResponse<string>> DeleteUser(User user);
    }



}
