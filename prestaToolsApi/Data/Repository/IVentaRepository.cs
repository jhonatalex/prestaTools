using Google.Protobuf.WellKnownTypes;
using prestaToolsApi.ModelsEntity;

namespace prestaToolsApi.Data.Repository
{
    public interface IVentaRepository
    {
        Task<ApiResponse<ResponseTransaction>> iniciar(PayData payData);
        Task<ApiResponse<DetalleVentum>> insertar(DetalleVentum detalleVenta);
        Task<ApiResponse<Ventum>> confirmar(Token tokenPasarela);
        Task<ApiResponse<Ventum>> sendEmail(EmailDTO emailObject);
    }
}
