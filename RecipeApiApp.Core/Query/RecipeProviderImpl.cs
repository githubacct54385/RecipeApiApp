using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApiApp.Core.ApiConfig;
using RecipeApiApp.Core.Errors;
using RecipeApiApp.Core.Models;

namespace RecipeApiApp.Core.Query {
    public sealed class RecipeProviderImpl : IRecipeProvider {
        private readonly IErrorWriter _errorWriter;
        private readonly IApiConfigProvider _apiConfigProvider;
        private readonly IRecipeSearchProvider _recipeSearchProvider;

        public RecipeProviderImpl (IErrorWriter errorWriter, IApiConfigProvider apiConfigProvider, IRecipeSearchProvider recipeSearchProvider) {
            _errorWriter = errorWriter;
            _apiConfigProvider = apiConfigProvider;
            _recipeSearchProvider = recipeSearchProvider;
        }

        public async Task<RecipePayload>
            GetRecipientsFromSearch (SearchParams searchParams) {

                // error handling
                if (string.IsNullOrEmpty (searchParams.SearchTerm)) {
                    return BadSearchParameterResult (searchParams.SearchTerm, "Search term cannot be empty or null.");
                }

                if (searchParams.From < 0) {
                    return BadSearchParameterResult (searchParams.SearchTerm, "Parameter From is less than zero.  Please provide a value greater than or equal to zero and less than the To parameter.");
                } else if (searchParams.To < 0) {
                    return BadSearchParameterResult (searchParams.SearchTerm, "Parameter To is less than zero.  Please provide a value greater than the To parameter.");
                } else if (searchParams.From >= searchParams.To) {
                    return BadSearchParameterResult (searchParams.SearchTerm, "Parameter From is greater than or equal to parameter To.  Please provide a From parameter that is less that the To parameter.");
                }

                string appId = _apiConfigProvider.GetApiConfigSettings ().AppId;
                string appKey = _apiConfigProvider.GetApiConfigSettings ().AppKey;
                string slackSecret = _apiConfigProvider.GetApiConfigSettings ().SlackSecret;

                if (string.IsNullOrEmpty (appId)) {
                    return MissingConfigurationResult (searchParams.SearchTerm, "Configuration Error: Missing AppId.");
                }
                if (string.IsNullOrEmpty (appKey)) {
                    return MissingConfigurationResult (searchParams.SearchTerm, "Configuration Error: Missing AppKey.");
                }
                if (string.IsNullOrEmpty (slackSecret)) {
                    return MissingConfigurationResult (searchParams.SearchTerm, "Configuration Error: Missing SlackSecret.");
                }

                RecipePayload data = await _recipeSearchProvider.RunSearch (searchParams, appId, appKey);
                return data;
            }

        private RecipePayload MissingConfigurationResult (string searchTerm, string warningMsg) {
            RecipePayload errorPayload = new RecipePayload ();
            errorPayload.Count = 0;
            errorPayload.Q = searchTerm;
            errorPayload.From = 0;
            errorPayload.To = 10;
            errorPayload.Warning = warningMsg;
            errorPayload.Hits = new List<Hit> ();
            return errorPayload;
        }

        private RecipePayload BadSearchParameterResult (string searchTerm, string warningMsg) {
            RecipePayload errorPayload = new RecipePayload ();
            errorPayload.Count = 0;
            errorPayload.Q = searchTerm;
            errorPayload.From = 0;
            errorPayload.To = 10;
            errorPayload.Warning = warningMsg;
            errorPayload.Hits = new List<Hit> ();
            return errorPayload;
        }
    }
}