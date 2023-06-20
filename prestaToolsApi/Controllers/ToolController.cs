using Microsoft.AspNetCore.Cors;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prestaToolsApi.ModelsEntity;


//using prestaToolsApi.model;
//using prestaToolsApi.models_DB;
using System.ComponentModel;

namespace prestaToolsApi.Controllers
{


    //[EnableCors("ReglasCors")]
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
            List<Tool> toolList = new List<Tool>();
            try
            {
                toolList = _dbContext.Tools.Include(c => c.objetoCategoria).ToList();

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

            try
            {
                _dbContext.Tools.Add(objectoTool);
                _dbContext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { message = "insertado Correctamente" });
            }
            catch (Exception e)
            {

                Exception innerException = e.InnerException;
                return StatusCode(StatusCodes.Status200OK, new { message = innerException.Message });
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


     



        
    */


    }



}
