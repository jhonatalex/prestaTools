using prestaToolsApi.ModelsEntity;

namespace prestaToolsApi.Data.Repository
{
    public interface ILenderRepository
    {
        Task<ApiResponse<List<Lender>>> GetAllLender();
        Task<ApiResponse<Lender>> GetByLenderId(int identifier);
        Task<ApiResponse<Lender>> InsertLender(Lender lender);
        Task<ApiResponse<Lender>> LoginLender(string email, string password);
        Task<ApiResponse<Lender>> UpdateLender(Lender lender);
        Task<ApiResponse<string>> DeleteLender(Lender lender);
    }
}
