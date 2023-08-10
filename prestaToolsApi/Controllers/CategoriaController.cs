using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prestaToolsApi.Data.Repository;
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

        private readonly ICategoriaRepository _categoryRepository;
        public CategoriaController(ICategoriaRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Route("list")]

        public async Task<IActionResult> getAllCategories()
        {
            return Ok(await _categoryRepository.GetAllCategory());
        }

        [HttpGet]
        [Route("get/{idCat:int}")]
        public async Task<IActionResult> getCategoryId(int idCat)
        {

            return Ok(await _categoryRepository.GetByCategoryId(idCat));

        }

        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> InsertCategory([FromBody] Category category)
        {

            return Ok(await _categoryRepository.InsertCategory(category));

        }

        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> editCategory([FromBody] Category category)
        {

            return Ok(await _categoryRepository.UpdateCategory(category));

        }

        [HttpDelete]
        [Route("delete/{idCat:int}")]
        public async Task<IActionResult> deleteCategory(int idCat)
        {
            return Ok(await _categoryRepository.DeleteCategory(new Category { IdCat = idCat }));
        }
        
        //[HttpPost]
        //[Route("save-imagen-cat")]
        //[DisableRequestSizeLimit,RequestFormLimits(MultipartBodyLengthLimit =int.MaxValue, ValueLengthLimit = int.MaxValue)]
        //public async Task<String> saveImage([FromForm] FileEntity file)
        //{
        //    try
        //    {

        //        return GuardarImagen(file.archivo);
        //    }
        //    catch (Exception e)
        //    {
        //        return e.Message;
        //    }
        //}
        //*/

        //private string GuardarImagen(IFormFile archivo)
        //{
        //    // Generar un nombre único para el archivo
        //    var nombreArchivo = $"{Guid.NewGuid()}{Path.GetExtension(archivo.FileName)}";

        //    // Obtener la ruta física donde se almacenarán las imágenes (por ejemplo, en una carpeta "Imagenes" dentro del directorio raíz de la aplicación)
        //    var rutaDirectorio = Path.Combine(Directory.GetCurrentDirectory(), "Imagenes/categoria");

        //    // Crear el directorio si no existe
        //    if (!Directory.Exists(rutaDirectorio))
        //    {
        //        Directory.CreateDirectory(rutaDirectorio);
        //    }

        //    // Construir la ruta completa donde se guardará el archivo
        //    var rutaArchivo = Path.Combine(rutaDirectorio, nombreArchivo);

        //    // Guardar el archivo en el servidor
        //    using (var fileStream = new FileStream(rutaArchivo, FileMode.Create))
        //    {
        //        archivo.CopyTo(fileStream);
        //    }

        //    // Devolver la ruta de la imagen guardada
        //    return rutaArchivo;
        //}

    }

}
