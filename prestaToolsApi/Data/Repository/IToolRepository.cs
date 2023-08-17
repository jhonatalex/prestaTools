using prestaToolsApi.ModelsEntity;

namespace prestaToolsApi.Data.Repository
{
    public interface IToolRepository
    {

        Task<ApiResponse<List<Tool>>> GetAllTool();
        Task<ApiResponse<Tool>> GetByToolId(int identifier);
        Task<ApiResponse<Tool>> InsertTool(Tool tool);
        Task<ApiResponse<Tool>> UpdateTool(Tool tool);
        Task<ApiResponse<string>> DeleteTool(Tool tool);

    }
}
