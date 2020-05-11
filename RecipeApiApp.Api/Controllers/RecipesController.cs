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

                switch (setting) {
                    case RuntimeSetting.Production:
                        // setup for prod
                        recipeSearchHandler = new ProdRecipeSearchHandlerImpl (_configuration);
                        break;
                    case RuntimeSetting.Development:
                        // setup for dev
                        recipeSearchHandler = new DevRecipeSearchHandlerImpl (_configuration);
                        break;
                    case RuntimeSetting.Both:
                        return RuntimeMisconfiguration (RuntimeSetting.Both, searchTerm, from, to);
                    case RuntimeSetting.Neither:
                        return RuntimeMisconfiguration (RuntimeSetting.Neither, searchTerm, from, to);
                    default:
                        return RuntimeMisconfiguration (RuntimeSetting.Unknown, searchTerm, from, to);
                }

                SearchParams searchParams = new SearchParams (searchTerm, from, to);

                RecipePayload payload = await recipeSearchHandler.Search (searchParams);
                return payload;
            } catch (System.Exception ex) {
                HandleEx (ex);
                return RecipeErrorResponses.ExceptionResponse (ex, searchTerm);
            }
        }

        private RecipePayload RuntimeMisconfiguration (RuntimeSetting runtimeSetting, string searchTerm, int from, int to) {
            RecipePayload payload = new RecipePayload ();
            payload.Q = searchTerm;
            payload.From = from;
            payload.To = to;
            payload.More = false;
            payload.Hits = new List<Hit> ();
            payload.Count = 0;

            switch (runtimeSetting) {
                case RuntimeSetting.Both:
                    payload.Warning = "Configuration Error: Both Dev and Prod config are set.";
                    break;
                case RuntimeSetting.Neither:
                    payload.Warning = "Configuration Error: Neither Dev and Prod config are set.";
                    break;
                case RuntimeSetting.Unknown:
                    payload.Warning = "Configuration Error: Unknown Config setting.";
                    break;
                default:
                    payload.Warning = "Configuration Error: Unknown Config setting.";
                    break;
            }
            return payload;
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