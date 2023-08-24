using Google.Protobuf.WellKnownTypes;
using prestaToolsApi.ModelsEntity;

namespace prestaToolsApi.Data.Repository
{
    public interface IVentaRepository
    {
        Task<ApiResponse<ResponseTransaction>> iniciar(PayData payData);
        Task<ApiResponse<DetalleVentum>> insertar(DetalleVentum detalleVenta);
        Task<ApiResponse<ResponseCommit>> confirmar(PayData payData);
    }
}
