using System.Threading.Tasks;
using RecipeApiApp.Core.ApiConfig;
using RecipeApiApp.Core.Env;
using RecipeApiApp.Core.Errors;
using RecipeApiApp.Core.Models;

namespace RecipeApiApp.Core.Query {
    public sealed class ProdRecipeSearchHandlerImpl : IRecipeSearchHandler {
        private readonly IErrorWriter _errorWriter;
        public ProdRecipeSearchHandlerImpl () {
            _errorWriter = new SlackChatWriter (new EnvironmentVarsConfigProviderImpl ());
        }
        public async Task<RecipePayload> Search (string searchTerm) {
            RecipieProviderImpl recipieProvider = new RecipieProviderImpl (_errorWriter);

            RecipeLookup recipeLookup =
                new RecipeLookup (recipieProvider);

            RecipePayload recipePayload =
                await recipeLookup.SearchRecipes (searchTerm);
            return recipePayload;
        }
    }
}