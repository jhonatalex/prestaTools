using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using prestaToolsApi.models_DB;
using System.ComponentModel;

namespace prestaToolsApi.Controllers
{
    [ApiController]
    [Route("api/tool")]
    public class ToolController : ControllerBase
    {
        public readonly PrestatoolsContext _dbContext;
        public ToolController(PrestatoolsContext dbContext) {
        
        _dbContext = dbContext;

        }


        [HttpGet]
        [Route("list")]
        public IActionResult List()
        {
            List<ToolEntity> toolList = new List<ToolEntity>();
            try
            {
                toolList = _dbContext.Tools.ToList();     //Include(c => c.ObjetCategory).ToList();

                return StatusCode(StatusCodes.Status200OK, new { message="ok",Response = toolList});
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = e.Message, });
            }
        }

        [HttpGet]
        [Route("get/{idTool:int}")]
        public IActionResult getTool(int idTool)
        {
            ToolEntity oTool = new ToolEntity();

            oTool = _dbContext.Tools.Find(idTool);

            if (oTool == null) {
                return BadRequest("Herramienta no encontrada");
            }

            try
            {
                oTool = _dbContext.Tools.Include(c => c.ObjetCategory).Where(ObjetoTool=> ObjetoTool.Id == idTool).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", Response = oTool });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = error.Message });
            }
        }


        [HttpPost]
        [Route("insert")]
        public IActionResult insertTool([FromBody] ToolEntity objectTool)
        {

            try
            {
                _dbContext.Tools.Add(objectTool);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { message = "insertado Correctamente" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = e.Message });
            }
        }


        [HttpPost]
        [Route("save-imagen-tool")]
        public async Task<String> saveImage([FromBody] ImageTool file)
        {

            try
            {
                var ruta = String.Empty;

                if (file.archivo.Length > 0)
                {

                    var nameFile = Guid.NewGuid().ToString() + ".jpg";
                    ruta = $"Imagenes/{nameFile}";

                    using (var stream = new FileStream(ruta, FileMode.Create)) {
                        await file.archivo.CopyToAsync(stream);
                    
                    }

                }

                return ruta;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }









    }
}
