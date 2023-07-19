using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using prestaToolsApi.Data.Repository;
using prestaToolsApi.ModelsEntity;

namespace prestaToolsApi.Controllers
{

    [EnableCors("ReglasCors")]
    [ApiController]
    [Route("api/lender")]
    public class LenderController : Controller
    {

        private readonly ILenderRepository _lenderRepository;
        public LenderController(ILenderRepository lenderRepository)
        {
            _lenderRepository = lenderRepository;

        }


        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> getAllLender()
        {

            return Ok(await _lenderRepository.GetAllLender());

        }

        [HttpGet]
        [Route("get/lenders/{id}")]
        public async Task<IActionResult> getLenderId(int id)
        {

            return Ok(await _lenderRepository.GetByLenderId(id));

        }

        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> InsertLender([FromBody] Lender lender)
        {

            if (lender == null)
            {
                return BadRequest();

            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            var response = await _lenderRepository.InsertLender(lender);
            return Ok(response);

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginLender([FromBody] Login lenderpayload)
        {
            var response = await _lenderRepository.LoginLender(lenderpayload.Email, lenderpayload.Password);
            return Ok(response);
        }


        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateLender([FromBody] Lender lender)
        {

            if (lender == null)
            {
                return BadRequest();

            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            var response = await _lenderRepository.UpdateLender(lender);
            return Ok(response);

        }

        [HttpDelete]
        [Route("delete/{email}")]
        public async Task<IActionResult> DeleteLender(string email)
        {

            return Ok(await _lenderRepository.DeleteLender(new Lender { Email = email }));

        }

    }

}
