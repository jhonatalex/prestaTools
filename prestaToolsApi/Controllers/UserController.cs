using Microsoft.AspNetCore.Mvc;
using prestaToolsApi.Data.Repository;
using System.Security.Claims;

namespace prestaToolsApi.Controllers
{


    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }




        [HttpGet]
        [Route("listar")]
        public async Task<IActionResult> getAllUsers() 
        {

            return Ok(await  _userRepository.GetAllUser());

        }

        [HttpGet]
        [Route("obtener/{id}")]
        public async Task<IActionResult> getUserId( int id)
        {

            return Ok(await _userRepository.GetByUserId(id));

        }



        [HttpPost]
        [Route("insertar")]
        public async Task<IActionResult> InsertUser([FromBody] User user)
        {
            //TODO. 

            if (user == null)
            {
                return BadRequest();

            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            var created = await _userRepository.InsertUser(user); 


            return Created("created", created);

        }



        [HttpPut]
        [Route("actualizar")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            //TODO. 

            if (user == null)
            {
                return BadRequest();

            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            await _userRepository.UpdateUser(user);


            return NoContent();

        }



        [HttpDelete]
        [Route("borrar/{id}")]
        public async Task<IActionResult> DeleteUser(int id) 
        {
            //await _userRepository.DeleteUser(new User { Id = id });
            return NoContent();
        }

    }



};
