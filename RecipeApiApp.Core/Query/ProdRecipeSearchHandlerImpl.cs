using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using RecipeApiApp.Core.ApiConfig;
using RecipeApiApp.Core.Env;
using RecipeApiApp.Core.Errors;
using RecipeApiApp.Core.Models;

namespace RecipeApiApp.Core.Query {
    public sealed class ProdRecipeSearchHandlerImpl : IRecipeSearchHandler {
        private readonly IErrorWriter _errorWriter;
        private readonly IConfiguration _configuration;
        public ProdRecipeSearchHandlerImpl (IConfiguration configuration) {
            _configuration = configuration;
            _errorWriter = new SlackChatWriter (new EnvironmentVarsConfigProviderImpl (configuration));
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