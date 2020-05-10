using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RecipeApiApp.Core.ApiConfig;
using RecipeApiApp.Core.Errors;
using RecipeApiApp.Core.Models;

namespace RecipeApiApp.Core.Query {
    public sealed class DevRecipeSearchHandlerImpl : IRecipeSearchHandler {
        private readonly IErrorWriter _errorWriter;
        private readonly IConfiguration _configuration;
        public DevRecipeSearchHandlerImpl (IConfiguration configuration) {
            _errorWriter = new SlackChatWriter (new ApiConfigProviderImpl ());
            _configuration = configuration;
        }
        public async Task<RecipePayload> Search (string searchTerm) {
            RecipieProviderImpl recipieProvider = new RecipieProviderImpl (_errorWriter, _configuration);

            RecipeLookup recipeLookup =
                new RecipeLookup (recipieProvider);

            RecipePayload recipePayload =
                await recipeLookup.SearchRecipes (searchTerm);
            return recipePayload;
        }
    }
}