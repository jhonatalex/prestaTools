using Google.Protobuf.WellKnownTypes;
using prestaToolsApi.ModelsEntity;

namespace prestaToolsApi.Data.Repository
{
    public interface IPaymentRepository
    {
        Task<ApiResponse<ResponseTransaction>> initTransaction(PayData payData);
    }
}
