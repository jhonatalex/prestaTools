using Microsoft.AspNetCore.Mvc;
using prestaToolsApi.model;

namespace prestaToolsApi.Data.Repository
{
    public interface ILenderRepository
    {

        Task<IActionResult> getAllLenders();
        Task<IActionResult> getLenderId(int id);
        Task<IActionResult> InsertLender([FromBody] Lender lender);
        Task<IActionResult> LoginLender(string email, string password);
        Task<IActionResult> UpdateLender([FromBody] Lender lender)
        Task<IActionResult> DeleteLender(int id);

    }
}
