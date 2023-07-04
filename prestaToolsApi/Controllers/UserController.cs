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

            return Ok(await  _userRepository.GetAllUser());

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
            //TODO. 

            if (user == null)
            {
                return BadRequest();

            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            var respponseBD = await _userRepository.InsertUser(user);

      
            return Ok(respponseBD);

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody] Login userpayload)
        {
       



            var response = await _userRepository.LoginUser(userpayload.Email, userpayload.Password);

            if (response.user == null)
            {
            
                return StatusCode(StatusCodes.Status401Unauthorized, response);
      
            }
            else
            {
        
                return Ok(response);
            }




        }




        [HttpPut]
        [Route("update")]
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
        [Route("delete/{email}")]
        public async Task<IActionResult> DeleteUser(string email) 

        {
            try
            {
                Boolean resp = await _userRepository.DeleteUser(new User { Email = email });

                if (resp == true)
                {

                    return StatusCode(StatusCodes.Status200OK, new { message = "Usuario ${email} Borrado Satisfactoriamente" });

                }
                else
                {
                    return BadRequest("Usuario  no encontrado");
                }


            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = error.Message });
            }


          
        }


               

    }

   

};
