using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using prestaToolsApi.Data.Repository;
using prestaToolsApi.ModelsEntity;

namespace prestaToolsApi.Controllers
{


    [EnableCors("ReglasCors")]
    [ApiController]
    [Route("api/user")]
    public class UserController : Controller
    {
 
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }


        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> getAllUsers() 
        {

            return Ok(await _userRepository.GetAllUser());

        }

        [HttpGet]
        [Route("get/users/{id}")]
        public async Task<IActionResult> getUserId( int id)
        {

            return Ok(await _userRepository.GetByUserId(id));

        }

        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> InsertUser([FromBody] User user)
        {

            if (user == null)
            {
                return BadRequest();

            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            var response = await _userRepository.InsertUser(user);
            return Ok(response);

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody] Login userpayload)
        {
            var response = await _userRepository.LoginUser(userpayload.Email, userpayload.Password);
            return Ok(response);
        }


        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {

            if (user == null)
            {
                return BadRequest();

            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            var response = await _userRepository.UpdateUser(user);
            return Ok(response);

        }

        [HttpDelete]
        [Route("delete/{email}")]
        public async Task<IActionResult> DeleteUser(string email) 
        {

            return Ok(await _userRepository.DeleteUser(new User { Email = email }));

        }            

    }

};
