using Microsoft.AspNetCore.Mvc;
using prestaToolsApi.Data.Repository;
using System.Security.Claims;

namespace prestaToolsApi.Controllers
{


    [ApiController]
    [Route("User")]
    public class UserController : Controller
    {


        private readonly InterfaceUserRepository _userRepository;

        public UserController(InterfaceUserRepository userRepository)
        {
            _userRepository = userRepository;

        }




        [HttpGet]
        [Route("listar")]
        public async Task<IActionResult> getAllUsers() {

         

        return Ok(await  _userRepository.GetAllUser());

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> getUserId( int id)
        {

         

            return Ok(await _userRepository.GetByUserId(id));

        }



        [HttpPost]
        [Route("")]
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

    }



};
