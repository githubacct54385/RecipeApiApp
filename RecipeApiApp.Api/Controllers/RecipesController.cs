using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeApiApp.Core.ApiConfig;
using RecipeApiApp.Core.Env;
using RecipeApiApp.Core.Errors;
using RecipeApiApp.Core.Models;
using RecipeApiApp.Core.Query;

namespace RecipeApiApp.Api.Controllers {
    [ApiController]
    [Route ("[controller]")]
    public class RecipesController : ControllerBase {
        [HttpGet]
        [Route ("Search/{searchTerm}")]
        public async Task<RecipePayload> SearchRecipes (string searchTerm) {
            try {

                RuntimeSetting setting = GetRuntimeSetting ();
                IRecipeSearchHandler recipeSearchHandler;
                if (setting == RuntimeSetting.Development) {
                    // setup for dev
                    recipeSearchHandler = new DevRecipeSearchHandlerImpl ();
                } else {
                    // setup for prod
                    recipeSearchHandler = new ProdRecipeSearchHandlerImpl ();
                }

                RecipePayload payload = await recipeSearchHandler.Search (searchTerm);
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
                errorWriter = new SlackChatWriter (new ApiConfigProviderImpl ());
            } else {
                errorWriter = new SlackChatWriter (new EnvironmentVarsConfigProviderImpl ());
            }
            errorWriter.Write (ex);
        }

        private static RuntimeSetting GetRuntimeSetting () {
            RecipeApiEnv apiEnv = new RecipeApiEnv (new RuntimeEnvProviderImpl ());
            return apiEnv.GetSettings ();
        }
    }
}