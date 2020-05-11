using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RecipeApiApp.Core.ApiConfig;
using RecipeApiApp.Core.Env;
using RecipeApiApp.Core.Errors;
using RecipeApiApp.Core.Models;
using RecipeApiApp.Core.Query;

namespace RecipeApiApp.Api.Controllers {
    [ApiController]
    [Route ("[controller]")]
    public class RecipesController : ControllerBase {
        private readonly IConfiguration _configuration;
        public RecipesController (IConfiguration configuration) {
            _configuration = configuration;
        }

        [HttpGet]
        [Route ("Search/{searchTerm}/{from}/{to}")]
        public async Task<RecipePayload> SearchRecipes (string searchTerm, int from = 0, int to = 10) {
            try {
                RuntimeSetting setting = GetRuntimeSetting ();
                IRecipeSearchHandler recipeSearchHandler;
                if (setting == RuntimeSetting.Development) {
                    // setup for dev
                    recipeSearchHandler = new DevRecipeSearchHandlerImpl (_configuration);
                } else {
                    // setup for prod
                    recipeSearchHandler = new ProdRecipeSearchHandlerImpl (_configuration);
                }

                SearchParams searchParams = new SearchParams (searchTerm, from, to);

                RecipePayload payload = await recipeSearchHandler.Search (searchParams);
                return payload;
            } catch (System.Exception ex) {
                HandleEx (ex);
                return RecipeErrorResponses.ExceptionResponse (ex, searchTerm);
            }
        }

        private void HandleEx (Exception ex) {
            RuntimeSetting setting = GetRuntimeSetting ();
            IErrorWriter errorWriter;
            if (setting == RuntimeSetting.Development) {
                errorWriter = new SlackChatWriter (new ApiConfigProviderImpl (_configuration));
            } else {
                errorWriter = new SlackChatWriter (new EnvironmentVarsConfigProviderImpl (_configuration));
            }
            errorWriter.WriteException (ex);
        }

        private RuntimeSetting GetRuntimeSetting () {
            RecipeApiEnv apiEnv = new RecipeApiEnv (new RuntimeEnvProviderImpl (_configuration));
            return apiEnv.GetSettings ();
        }
    }
}