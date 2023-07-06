using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prestaToolsApi.ModelsEntity;
using System;
using System.Security.AccessControl;

namespace prestaToolsApi.Controllers
{

   [EnableCors("ReglasCors")]
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


            /*
            if (objectCat.imagen1File == null || objectCat.imagen1File.Length == 0)
            {
                return BadRequest("No se ha enviado ninguna imagen.");
            }

            */
           // var rutaImagen = GuardarImagen(objectCat.imagen1File); // Lógica para guardar la imagen



            try
            {
                //objectCat.UrlImagen = rutaImagen;

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
            var existingCategory = _dbContext.Categories.Find(objectCat.IdCat);

            if (existingCategory == null)
            {
                return BadRequest("Categoría no encontrada");
            }

            try
            {
                existingCategory.TitleCat = objectCat.TitleCat is null? existingCategory.TitleCat: objectCat.TitleCat;
                existingCategory.DescripCat = objectCat.DescripCat is null ? existingCategory.DescripCat : objectCat.DescripCat;
                existingCategory.UrlImagen = objectCat.UrlImagen is null ? existingCategory.UrlImagen: objectCat.UrlImagen;
                existingCategory.UrlImagenBanner = objectCat.UrlImagenBanner is null ?  existingCategory.UrlImagenBanner: objectCat.UrlImagenBanner;



                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { message = "Categoría actualizada correctamente" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }

       


        [HttpDelete]
        [Route("delete/{idcat:int}")]
        public IActionResult deleteCat(int idCat)
        {
            Category oCategori = _dbContext.Categories.Find(idCat);



            if (oCategori == null)
            {
                return BadRequest("categoria no encontrada");
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

        /*

        [HttpPost]
        [Route("save-imagen-cat")]
        [DisableRequestSizeLimit,RequestFormLimits(MultipartBodyLengthLimit =int.MaxValue, ValueLengthLimit = int.MaxValue)]
        public async Task<String> saveImage([FromForm] FileEntity file)
        {

            try
            {

                return GuardarImagen(file.archivo);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        */


        private string GuardarImagen(IFormFile archivo)
        {
            // Generar un nombre único para el archivo
            var nombreArchivo = $"{Guid.NewGuid()}{Path.GetExtension(archivo.FileName)}";

            // Obtener la ruta física donde se almacenarán las imágenes (por ejemplo, en una carpeta "Imagenes" dentro del directorio raíz de la aplicación)
            var rutaDirectorio = Path.Combine(Directory.GetCurrentDirectory(), "Imagenes/categoria");

            // Crear el directorio si no existe
            if (!Directory.Exists(rutaDirectorio))
            {
                Directory.CreateDirectory(rutaDirectorio);
            }

            // Construir la ruta completa donde se guardará el archivo
            var rutaArchivo = Path.Combine(rutaDirectorio, nombreArchivo);

            // Guardar el archivo en el servidor
            using (var fileStream = new FileStream(rutaArchivo, FileMode.Create))
            {
                archivo.CopyTo(fileStream);
            }

            // Devolver la ruta de la imagen guardada
            return rutaArchivo;
        }








    }



}
