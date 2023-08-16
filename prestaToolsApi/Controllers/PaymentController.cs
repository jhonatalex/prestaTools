using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using prestaToolsApi.Data.Repository;
using prestaToolsApi.ModelsEntity;

namespace prestaToolsApi.Controllers
{

    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentController(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;

        }

        [HttpPost]
        [Route("insertar-transaccion")]
        public async Task<IActionResult> Iniciar([FromBody] Ventum venta)
        {
            var response = await _paymentRepository.InsertarVenta(venta);
            return Ok(response);
        }
    }
}
