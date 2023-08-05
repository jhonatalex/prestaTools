using prestaToolsApi.ModelsEntity;

namespace prestaToolsApi.Data.Repository
{
    public interface ICategoriaRepository
    {
        Task<ApiResponse<List<Category>>> GetAllCategory();
        Task<ApiResponse<Category>> GetByCategoryId(int identifier);
        Task<ApiResponse<Category>> InsertCategory(Category category);
        Task<ApiResponse<Category>> UpdateCategory(Category category);
        Task<ApiResponse<string>> DeleteCategory(Category category);
    }
}
