using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RecipeApiApp.Core.ApiConfig;
using RecipeApiApp.Core.Env;
using RecipeApiApp.Core.Errors;
using RecipeApiApp.Core.Models;

namespace RecipeApiApp.Core.Query {
    public sealed class ProdRecipeSearchHandlerImpl : IRecipeSearchHandler {
        private readonly IErrorWriter _errorWriter;
        public ProdRecipeSearchHandlerImpl () {
            IList<IConfigurationProvider> providers = new List<IConfigurationProvider> ();
            ConfigurationRoot root = new ConfigurationRoot (providers);
            _errorWriter = new SlackChatWriter (new EnvironmentVarsConfigProviderImpl (root));
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