using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prestaToolsApi.ModelsEntity;
using System.Security.AccessControl;

namespace prestaToolsApi.Controllers
{

    //[EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        

        public readonly PrestatoolsContext _dbContext;
        public CategoriaController(PrestatoolsContext dbContext)
        {

            _dbContext = dbContext;

        }


        [HttpGet]
        [Route("list")]
        public IActionResult List()
        {
            List<Category> catList = new List<Category>();
            try
            {
                catList = _dbContext.Categories.ToList();

                return StatusCode(StatusCodes.Status200OK, new { message = "ok", Response = catList });
            }
            catch (Exception e)
            {

                return StatusCode(StatusCodes.Status200OK, new { message = e.Message, });

            }
        }

        [HttpGet]
        [Route("get/{idCat:int}")]
        public IActionResult getCat(int idCat)
        {
            Category oCat = new Category();

            oCat = _dbContext.Categories.Find(idCat);

            if (oCat == null)
            {
                return BadRequest("Categoria no encontrada");
            }

            try
            {
                oCat = _dbContext.Categories.Where(oCat => oCat.IdCat == idCat).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", Response = oCat });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = error.Message });
            }
        }


        [HttpPost]
        [Route("insert")]
        public IActionResult insertCat([FromBody] Category objectCat)
        {

            try
            {
                _dbContext.Categories.Add(objectCat);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { message = "insertado Correctamente la categoria" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = e.Message });
            }
        }



        
        [HttpPut]
        [Route("edit")]
        public IActionResult editTool([FromBody] Category objectCat)
        {
            Category oCategory = new Category();

            oCategory = _dbContext.Categories.Find(objectCat.IdCat);

            if (oCategory == null)
            {
                return BadRequest("Herramienta no encontrada");
            }


            try
            {
                objectCat.TitleCat = oCategory .TitleCat is null ? objectCat.TitleCat : oCategory.TitleCat;
                objectCat.DescripCat = oCategory.DescripCat is null ? objectCat.DescripCat : oCategory.DescripCat;
                objectCat.UrlImagen = oCategory.UrlImagen is null ? objectCat.UrlImagen : oCategory.UrlImagen;
                objectCat.UrlImagenBanner = oCategory .UrlImagenBanner is null ? objectCat.UrlImagenBanner : oCategory .UrlImagenBanner;
          
      

                _dbContext.Categories.Update(oCategory);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { message = "Categoria Actualizado Correctamente" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = e.Message });
            }
        }

       



        [HttpDelete]
        [Route("delete/{idcat:int}")]
        public IActionResult deleteCat(int idCat)
        {
            Category oCategori = _dbContext.Categories.Find(idCat);



            if (oCategori == null)
            {
                return BadRequest("categiria no encontrada");
            }
            try
            {

                _dbContext.Categories.Remove(oCategori);
                _dbContext.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { message = "ok", Response = oCategori });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = error.Message });
            }
        }




       




            

    }



}
