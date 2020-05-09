using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeApiApp.Core.Models;
using RecipeApiApp.Core.Query;

namespace RecipeApiApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipesController : ControllerBase
    {
        [HttpGet]
        [Route("SearchRecipes/{searchTerm}")]
        public async Task<RecipePayload> SearchRecipes(string searchTerm)
        {
            RecipeLookup recipeLookup =
                new RecipeLookup(new RecipieProviderImpl());
            RecipePayload recipePayload =
                await recipeLookup.SearchRecipes(searchTerm);
            return recipePayload;
        }
    }
}
