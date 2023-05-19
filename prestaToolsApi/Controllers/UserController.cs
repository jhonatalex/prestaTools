using Microsoft.AspNetCore.Mvc;

namespace prestaToolsApi.Controllers
{


    [ApiController]
    [Route("User")]
    public class UserController : Controller
    {



        [HttpGet]
        [Route("listar")]

        //CRUD



        public dynamic getUsers() {

            //TODO TODA LA LOGICA para buscar el usuarios en la base de datos



            List<User> users = new List<User>
            {

                new User
                {
                    id = 1,
                    name = "Hialmar",
                    email = "uno@uno.com",
                    password = "1234",
                },
                new User
                {
                    id = 2,
                    name = "RicARDO",
                    email = "DOS@uno.com",
                    password = "1234",
                },
                    new User
                {
                    id = 2,
                    name = "CARLOS",
                    email = "DOS@uno.com",
                    password = "1234",
                }
            };

        return users;

        }





    };




};
