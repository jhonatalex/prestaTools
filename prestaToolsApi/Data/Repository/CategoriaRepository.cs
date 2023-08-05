using Microsoft.EntityFrameworkCore;
using prestaToolsApi.ModelsEntity;

namespace prestaToolsApi.Data.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly PrestatoolsContext _context;

        //Declaración de variables para uso del ApiResponse
        string token = "tu_token";
        bool success;
        ErrorRes errorRes = new ErrorRes();
        string message;

        public CategoriaRepository(PrestatoolsContext context) 
        {
            _context = context;
        }

        ////////////////////////////////////////////////////////////////
        ///               GET ALL CATEGORIES
        ////////////////////////////////////////////////////////////////

        public async Task<ApiResponse<List<Category>>> GetAllCategory()
        {

            List<Category> categories = await _context.Categories.ToListAsync();

            try
            {

                if (categories.Count > 0)
                {
                    success = true;
                    message = "Categorías encontradas";
                }
                else
                {
                    success = true;
                    errorRes = new ErrorRes { /* establecer propiedades de ErrorRes */ };
                    message = "Categorías no encontradas";
                }

                var response = new ApiResponse<List<Category>>(categories, token, success, errorRes, message);
                return response;

            }

            catch (Exception ex)
            {
                success = false;
                errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };
                message = "Error";

                var response = new ApiResponse<List<Category>>(categories, token, success, errorRes, message);
                return response;
            }
        }

        ////////////////////////////////////////////////////////////////
        ///               GET BY CATEGORY ID
        ////////////////////////////////////////////////////////////////

        public async Task<ApiResponse<Category>> GetByCategoryId(int identifier)
        {
            Category categoryById = await _context.Categories.FirstOrDefaultAsync(u => u.IdCat == identifier);
            
            if (categoryById == null)
            {
                success = false;
                message = "Categoría no encontrada";
            }
            else
            {
                success = true;
                message = "Categoría encontrada";
            }

            var response = new ApiResponse<Category>(categoryById, token, success, errorRes, message);
            return response;
        }

        ////////////////////////////////////////////////////////////////
        ///               INSERT CATEGORY
        ////////////////////////////////////////////////////////////////

        public async Task<ApiResponse<Category>> InsertCategory(Category category)
        {

            try
            {

                _context.Categories.Add(category);

                int result = await _context.SaveChangesAsync();
                success = true;
                message = "Categoría creada satisfactoriamente";

                var response = new ApiResponse<Category>(null, token, success, errorRes, message);
                return response;

            }
            catch (Exception ex)
            {

                token = "n/a";
                success = false;
                errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };
                message = "Error al insertar categoría";

                var response = new ApiResponse<Category>(null, token, success, errorRes, message);
                return response;
            }

        }

        ////////////////////////////////////////////////////////////////
        ///               UPDATE CATEGORY
        ////////////////////////////////////////////////////////////////

        public async Task<ApiResponse<Category>> UpdateCategory(Category category)
        {

            _context.Categories.Update(category);
            int result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                success = true;
                message = "Categoría actualizada satisfactoriamente";
            }
            else
            {
                success = false;
                message = "Categoría no encontrada";
            }

            var response = new ApiResponse<Category>(null, token, success, errorRes, message);
            return response;

        }

        ////////////////////////////////////////////////////////////////
        ///               DELETE CATEGORY
        ////////////////////////////////////////////////////////////////

        public async Task<ApiResponse<string>> DeleteCategory(Category category)
        {

            try
            {
                _context.Categories.Remove(category);
                int result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    success = true;
                    message = "Categoría borrada satisfactoriamente";
                }
                else
                {
                    token = "n/a";
                    success = false;
                    message = "Categoría no encontrada";
                }

                var response = new ApiResponse<string>(null, token, success, errorRes, message);
                return response;
            }

            catch (Exception ex)
            {
                token = "n/a";
                success = false;
                message = "Error: Categoría no encontrada";
                var errorRes = new ErrorRes { code = ex.GetHashCode(), message = ex.Message };
                var response = new ApiResponse<string>(null, token, success, errorRes, message);
                return response;

            }

        }
    }
}
