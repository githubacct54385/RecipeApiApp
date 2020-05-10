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
        public ProdRecipeSearchHandlerImpl () {
            IList<IConfigurationProvider> providers = new List<IConfigurationProvider> ();
            providers.Add (new EnvironmentVariablesConfigurationProvider ());
            ConfigurationRoot root = new ConfigurationRoot (providers);
            Console.WriteLine (root.GetDebugView ());
            System.Diagnostics.Trace.TraceError ("If you're seeing this, something bad happened");
            Console.WriteLine ("Env App Id" + root["RecipeApi_AppId"]);
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