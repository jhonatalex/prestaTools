using prestaToolsApi.ModelsEntity;

namespace prestaToolsApi.Data.Repository
{
    public interface ILenderRepository
    {
        Task<IEnumerable<Lender>> GetAllLenders();
        Task<Lender> GetByLenderId(int id);
        Task<Lender> LoginLender(string email, string password);
        Task<bool> InsertLender(Lender lender);
        Task<bool> UpdateLender(Lender lender);
        Task<bool> DeleteLender(Lender lender);
        //Task DeleteLender(Lender lender);
    }
}
