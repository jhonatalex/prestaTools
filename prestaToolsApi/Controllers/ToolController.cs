using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prestaToolsApi.Data.Repository;
using prestaToolsApi.ModelsEntity;
using System.ComponentModel;

namespace prestaToolsApi.Controllers
{

    [EnableCors("ReglasCors")]
    [ApiController]
    [Route("api/tool")]
    public class ToolController : ControllerBase
    {

        private readonly IToolRepository _toolRepository;
        public ToolController(IToolRepository toolRepository)
        {
            _toolRepository = toolRepository;

        }

        [HttpGet]
        [Route("list")]

        public async Task<IActionResult> getAllTools()
        {

            return Ok(await _toolRepository.GetAllTool());

        }

        [HttpGet]
        [Route("get/{idTool:int}")]
        public async Task<IActionResult> getToolId(int idTool)
        {

            return Ok(await _toolRepository.GetByToolId(idTool));

        }

        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> InsertTool([FromBody] Tool tool)
        {

            return Ok(await _toolRepository.InsertTool(tool));

        }

        [HttpPut]
        [Route("edit")]

        public async Task<IActionResult> editTool([FromBody] Tool tool)
        {

            return Ok(await _toolRepository.UpdateTool(tool));

        }

        [HttpDelete]
        [Route("delete/{idTool:int}")]
        public async Task<IActionResult> deleteTool(int idTool)
        {
            return Ok(await _toolRepository.DeleteTool(new Tool { Id = idTool}));
        }

        //[HttpPost]
        //[Route("save-imagen-tool")]
        //[DisableRequestSizeLimit, RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue, ValueLengthLimit = int.MaxValue)]
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
        // }

        //    private string GuardarImagen(IFormFile archivo)
        //{
        //    // Generar un nombre único para el archivo
        //    var nombreArchivo = $"{Guid.NewGuid()}{Path.GetExtension(archivo.FileName)}";

        //    // Obtener la ruta física donde se almacenarán las imágenes (por ejemplo, en una carpeta "Imagenes" dentro del directorio raíz de la aplicación)
        //    var rutaDirectorio = Path.Combine(Directory.GetCurrentDirectory(), "Imagenes/tool");

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
