using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RecipeApiApp.Core.ApiConfig;
using RecipeApiApp.Core.Models;
using RestSharp;

namespace RecipeApiApp.Core.Query {
    public sealed class RecipieProviderImpl : IRecipeProvider {
        public RecipieProviderImpl () { }

        public async Task<RecipePayload>
            GetRecipientsFromSearch (string searchTerm) {
                if (string.IsNullOrEmpty (searchTerm)) {
                    return MissingSearchTermResponse (searchTerm);
                }
                RestClient client = new RestClient ("https://api.edamam.com");

                // get api settings from file system
                ApiConfigSettings apiSettings = ApiSettings ();

                string appId = apiSettings.AppId;
                string appKey = apiSettings.AppKey;

                RestRequest request = new RestRequest ("search", Method.GET);
                request.AddQueryParameter ("q", searchTerm);
                request.AddQueryParameter ("app_id", appId);
                request.AddQueryParameter ("app_key", appKey);

                IRestResponse<RecipePayload> response =
                    await client.ExecuteAsync<RecipePayload> (request);

                if (response.IsSuccessful) {
                    return response.Data;
                }
                return ErrorResponse (response, searchTerm);
            }

        private ApiConfigSettings ApiSettings () {
            IApiConfigRepository apiSettingsRepo =
                new ApiConfigRepositoryImpl (new ApiConfigProviderImpl ());

            return apiSettingsRepo.GetSettings ();
        }

        private RecipePayload MissingSearchTermResponse (string searchTerm) {
            RecipePayload errorPayload = new RecipePayload ();
            errorPayload.Count = 0;
            errorPayload.Q = searchTerm;
            errorPayload.From = 0;
            errorPayload.To = 10;
            errorPayload.Warning = "Search term cannot be emtpy or null.";
            errorPayload.Hits = new List<Hit> ();
            return errorPayload;
        }

        private RecipePayload
        ErrorResponse (IRestResponse<RecipePayload> response, string searchTerm) {
            RecipePayload errorPayload = new RecipePayload ();
            errorPayload.Count = 0;
            errorPayload.Q = searchTerm;
            errorPayload.From = 0;
            errorPayload.To = 10;
            errorPayload.Warning = response.ErrorMessage;
            errorPayload.Hits = new List<Hit> ();
            return errorPayload;
        }
    }
}