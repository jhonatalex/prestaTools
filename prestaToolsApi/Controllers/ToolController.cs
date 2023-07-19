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



        public readonly PrestatoolsContext _dbContext;
        public ToolController(PrestatoolsContext dbContext)
        {

            _dbContext = dbContext;

        }

        //private readonly IToolRepository _toolRepository;
        //public ToolController(IToolRepository toolRepository)
        //{
        //    _toolRepository = toolRepository;

        //}


        [HttpGet]
        [Route("list")]

        //public async Task<IActionResult> getAllTools()
        //{

        //    return Ok(await _toolRepository);

        //}

        public IActionResult List()
        {

            string token = "tu_token";
            bool success;
            ErrorRes errorRes = new ErrorRes();
            string message;

            List<Tool> toolList = new List<Tool>();
            try
            {
                toolList = _dbContext.Tools.Include(c => c.objetoCategoria).ToList();

                //return StatusCode(StatusCodes.Status200OK, new { message = "ok", Response = toolList });

                success = true;
                message = "Herramientas listadas correctamente";

                var response = new ApiResponse<List<Tool>>(toolList, token, success, errorRes, message);
                return Ok(response);

            }
            catch (Exception e)
            {
                //return StatusCode(StatusCodes.Status200OK, new { message = e.Message, });

                success = false;
                message = "Error al listar herramientas";

                var response = new ApiResponse<List<Tool>>(null, token, success, errorRes, message);
                return Ok(response);
            }

        }

        [HttpGet]
        [Route("get/{idTool:int}")]
        public IActionResult getTool(int idTool)
        {
            Tool oTool = new Tool();

            oTool = _dbContext.Tools.Find(idTool);

            if (oTool == null) {
                return BadRequest("Herramienta no encontrada");
            }

            try
            {
                oTool = _dbContext.Tools.Include(c => c.objetoCategoria).Where(ObjetoTool=> ObjetoTool.Id == idTool).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", Response = oTool });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = error.Message });
            }
        }


        [HttpPost]
        [Route("insert")]
        public IActionResult insertTool([FromBody] Tool objectoTool)
        {
            string token = "tu_token";
            bool success;
            ErrorRes errorRes = new ErrorRes();
            string message;

            try
            {

                objectoTool.DateUp = DateTime.Now.ToString("yyyy-MM-dd");

                _dbContext.Tools.Add(objectoTool);
                _dbContext.SaveChanges();


                

                success = true;
                message = "Herramienta insertada satisfactoriamente";

                var response = new ApiResponse<Tool>(null, token, success, errorRes, message);
                return Ok(response);

            }
            catch (Exception ex)
            {

                token = "n/a";
                success = false;
                errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };
                message = "Error al Insertar";

                var response = new ApiResponse<Tool>(null, token, success, errorRes, message);
                return Ok(response);
            }



        }


        [HttpPut]
        [Route("edit")]
        public IActionResult editTool([FromBody] Tool objectTool)
        {

            Tool oTool = new Tool();

            oTool = _dbContext.Tools.Find(objectTool.Id);

            if (oTool == null)
            {
                return BadRequest("Herramienta no encontrada");
            }


            try
            {
                objectTool.Name = oTool.Name is null ? objectTool.Name : oTool.Name;
                objectTool.Reference = oTool.Reference is null ? objectTool.Reference : oTool.Reference;
                objectTool.New = oTool.New;
                objectTool.Model = oTool.Model is null ? objectTool.Model : oTool.Model;
                objectTool.Description = oTool.Description is null ? objectTool.Description : oTool.Description;
                objectTool.Widgets = oTool.Widgets;
                objectTool.ValueCommercial = oTool.ValueCommercial;
                objectTool.ValueRent = oTool.ValueRent;
                objectTool.YearBuy = oTool.YearBuy;
                objectTool.Weigt = oTool.Weigt;
                objectTool.Mesuare = oTool.Mesuare;
                objectTool.NumberPiece = oTool.NumberPiece;
                objectTool.UrlImage = oTool.UrlImage is null ? objectTool.UrlImage : oTool.UrlImage;
                objectTool.UrlImage2 = oTool.UrlImage2 is null ? objectTool.UrlImage2 : oTool.UrlImage2;
                objectTool.UrlImage3 = oTool.UrlImage3 is null ? objectTool.UrlImage3 : oTool.UrlImage3;
                objectTool.TermsUse = oTool.TermsUse is null ? objectTool.TermsUse : oTool.TermsUse;
                objectTool.BreakDowns = oTool.BreakDowns is null ? objectTool.BreakDowns : oTool.BreakDowns;
                objectTool.TimeUse = oTool.TimeUse;
                objectTool.IdCategory = oTool.IdCategory;
                objectTool.IdLenders = oTool.IdLenders;
                objectTool.DateUp = oTool.DateUp;
                objectTool.DetalleVenta = oTool.DetalleVenta ?? objectTool.DetalleVenta;
                objectTool.objetoCategoria = oTool.objetoCategoria is null ? objectTool.objetoCategoria : oTool.objetoCategoria;
                objectTool.objetoLender = oTool.objetoLender is null ? objectTool.objetoLender : oTool.objetoLender;
                objectTool.objetoLender = oTool.objetoLender is null ? objectTool.objetoLender : oTool.objetoLender;


                _dbContext.Tools.Update(objectTool);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { message = "Actualizado Correctamente" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = e.Message });
            }
        }





        [HttpDelete]
        [Route("delete/{idTool:int}")]
        public IActionResult deleteTool(int idTool)
        {
            Tool oTool = new Tool();

            oTool = _dbContext.Tools.Find(idTool);

            if (oTool == null)
            {
                return BadRequest("Herramienta no encontrada");
            }
            try
            {

                _dbContext.Tools.Remove(oTool);
                _dbContext.SaveChanges();


                return StatusCode(StatusCodes.Status200OK, new { message = "ok", Response = oTool });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = error.Message });
            }
        }



        /*

        [HttpPost]
        [Route("save-imagen-tool")]
        [DisableRequestSizeLimit, RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue, ValueLengthLimit = int.MaxValue)]
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
            var rutaDirectorio = Path.Combine(Directory.GetCurrentDirectory(), "Imagenes/tool");

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
