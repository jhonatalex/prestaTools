using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using prestaToolsApi.Data.Repository;
using prestaToolsApi.ModelsEntity;

namespace prestaToolsApi.Controllers
{

    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : Controller
    {
        private readonly IVentaRepository _ventaRepository;
        public VentaController(IVentaRepository ventaRepository)
        {
            _ventaRepository = ventaRepository;

        }

        [HttpPost]
        [Route("iniciar-transaccion")]
        public async Task<IActionResult> iniciar([FromBody] PayData payData)
        {
            var response = await _ventaRepository.iniciar(payData);
            return Ok(response);
        }

        [HttpPost]
        [Route("insertar-venta")]
        public async Task<IActionResult> insertar([FromBody] DetalleVentum detalleVenta)
        {
            var response = await _ventaRepository.insertar(detalleVenta);
            return Ok(response);
        }

        [HttpPost]
        [Route("confirmar-transaccion")]
        public async Task<IActionResult> confirmar([FromBody] PayData payData)
        {
            var response = await _ventaRepository.confirmar(payData);
            return Ok(response);
        }
    }
}
