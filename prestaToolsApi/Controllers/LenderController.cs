﻿using Microsoft.AspNetCore.Mvc;
using prestaToolsApi.Data.Repository;
using prestaToolsApi.model;

namespace prestaToolsApi.Controllers
{

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
        public async Task<IActionResult> getAllLenders()
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
            //TODO. 

            if (user == null)
            {
                return BadRequest();

            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }

            var created = await _lenderRepository.InsertUser(lender);


            return Created("created", created);

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginLender(string email, string password)
        {
            //TODO. 

            if (email == null || password == null)
            {
                return BadRequest();

            }

            var user = await _lenderRepository.LoginLender(email, password);


            if (user == null)
            {
                return BadRequest();

            }


            return Created("created", lender);

        }




        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateLender([FromBody] Lender lender)
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

            await _lenderRepository.UpdateLender(lender);


            return NoContent();

        }



        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteLender(int id)
        {
            await _lenderRepository.DeleteLender(new Lender { id = id });

            return NoContent();
        }

    }
}
