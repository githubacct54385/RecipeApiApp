using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RecipeApiApp.Core.ApiConfig;
using RecipeApiApp.Core.Errors;
using RecipeApiApp.Core.Models;

namespace RecipeApiApp.Core.Query {
    public sealed class ProdRecipeSearchHandlerImpl : IRecipeSearchHandler {
        private readonly IErrorWriter _errorWriter;
        private readonly IConfiguration _configuration;
        private readonly IApiConfigProvider _apiConfigProvider;
        private readonly IRecipeSearchProvider _searchProvider;

        public ProdRecipeSearchHandlerImpl (IConfiguration configuration) {
            _configuration = configuration;
            _errorWriter = new SlackChatWriter (new EnvironmentVarsConfigProviderImpl (configuration));
            _searchProvider = new RecipeSearchProviderImpl (_errorWriter);
            _apiConfigProvider = new EnvironmentVarsConfigProviderImpl (_configuration);
        }
        public async Task<RecipePayload> Search (SearchParams searchParams) {
            RecipeProviderImpl recipieProvider = new RecipeProviderImpl (_errorWriter, _apiConfigProvider, _searchProvider);

            RecipeLookup recipeLookup =
                new RecipeLookup (recipieProvider, _configuration);

            RecipePayload recipePayload =
                await recipeLookup.SearchRecipes (searchParams);
            return recipePayload;
        }
    }
}