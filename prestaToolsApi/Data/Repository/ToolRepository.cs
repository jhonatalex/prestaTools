using Microsoft.EntityFrameworkCore;
using prestaToolsApi.ModelsEntity;

namespace prestaToolsApi.Data.Repository
{
    public class ToolRepository: IToolRepository
    {
        private readonly PrestatoolsContext _context;

        //Declaración de variables para uso del ApiResponse
        string token = "tu_token";
        bool success;
        ErrorRes errorRes = new ErrorRes();
        string message;

        public ToolRepository(PrestatoolsContext context)
        {
            _context = context;
        }

        ////////////////////////////////////////////////////////////////
        ///               GET ALL TOOLS
        ////////////////////////////////////////////////////////////////

        public async Task<ApiResponse<List<Tool>>> GetAllTool()
        {

            List<Tool> tools = await _context.Tools
                .Include(c => c.objetoCategoria)
                .ToListAsync();

            try
            {

                if (tools.Count > 0)
                {
                    success = true;
                    message = "Herramientas encontradas";
                }
                else
                {
                    success = true;
                    errorRes = new ErrorRes { /* establecer propiedades de ErrorRes */ };
                    message = "Herramientas no encontradas";
                }

                var response = new ApiResponse<List<Tool>>(tools, token, success, errorRes, message);
                return response;

            }

            catch (Exception ex)
            {
                success = false;
                errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };
                message = "Error";

                var response = new ApiResponse<List<Tool>>(tools, token, success, errorRes, message);
                return response;
            }
        }

        ////////////////////////////////////////////////////////////////
        ///               GET BY TOOL ID
        ////////////////////////////////////////////////////////////////

        public async Task<ApiResponse<Tool>> GetByToolId(int identifier)
        {
            Tool toolById = await _context.Tools.FirstOrDefaultAsync(u => u.Id == identifier);
            toolById = _context.Tools
                .Include(c => c.objetoCategoria)
                .Include(d => d.objetoLender)
                .Where(ObjetoTool => ObjetoTool.Id == identifier).FirstOrDefault();

            if (toolById == null)
            {
                success = false;
                message = "Herramienta no encontrada";
            }
            else
            {
                success = true;
                message = "Herramienta encontrada";
            }

            var response = new ApiResponse<Tool>(toolById, token, success, errorRes, message);
            return response;
        }

        ////////////////////////////////////////////////////////////////
        ///               INSERT TOOL
        ////////////////////////////////////////////////////////////////

        public async Task<ApiResponse<Tool>> InsertTool(Tool tool)
        {

            try
            {

                tool.DateUp = DateTime.Now.ToString("yyyy-MM-dd");

                var categoryExists = _context.Categories.Any(cat => cat.IdCat == tool.IdCategory);
                var lenderExists = _context.Lenders.Any(len => len.Email == tool.IdLenders);

                if (categoryExists) 
                {
                    if (lenderExists)
                    {
                        _context.Tools.Add(tool);
                        int result = await _context.SaveChangesAsync();
                        success = true;
                        message = "Herramienta creada satisfactoriamente";
                        var response = new ApiResponse<Tool>(null, token, success, errorRes, message);
                        return response;

                    }
                    else
                    {
                        success = false;
                        message = "Debe estar verificado para poder registrar una herramienta";
                        var response = new ApiResponse<Tool>(null, token, success, errorRes, message);
                        return response;

                    }
                }
                else
                {
                    success = false;
                    message = "La categoría no existe";
                    var response = new ApiResponse<Tool>(null, token, success, errorRes, message);
                    return response;
                }

            }
            catch (Exception ex)
            {

                token = "n/a";
                success = false;
                errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };
                message = "Error al insertar herramienta";

                tool = null;

                var response = new ApiResponse<Tool>(tool, token, success, errorRes, message);
                return response;
            }

        }

        ////////////////////////////////////////////////////////////////
        ///               UPDATE TOOL
        ////////////////////////////////////////////////////////////////

        public async Task<ApiResponse<Tool>> UpdateTool(Tool tool)
        {

            _context.Tools.Update(tool);
            int result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                success = true;
                message = "Herramienta actualizada satisfactoriamente";
            }
            else
            {
                success = false;
                message = "Herramienta no encontrada";
            }

            tool = null;

            var response = new ApiResponse<Tool>(tool, token, success, errorRes, message);
            return response;

        }

        ////////////////////////////////////////////////////////////////
        ///               DELETE TOOL
        ////////////////////////////////////////////////////////////////

        public async Task<ApiResponse<string>> DeleteTool(Tool tool)
        {

            try
            {
                _context.Tools.Remove(tool);
                int result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    success = true;
                    message = "Herramienta borrada satisfactoriamente";
                }
                else
                {
                    token = "n/a";
                    success = false;
                    message = "Herramienta no encontrada";
                }

                var response = new ApiResponse<string>(null, token, success, errorRes, message);
                return response;
            }

            catch (Exception ex)
            {
                token = "n/a";
                success = false;
                message = "Error: herramienta no encontrada";
                var errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };
                var response = new ApiResponse<string>(null, token, success, errorRes, message);
                return response;

            }

        }

    }

}
