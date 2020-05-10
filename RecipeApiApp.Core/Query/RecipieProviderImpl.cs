using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RecipeApiApp.Core.Errors;
using RecipeApiApp.Core.Models;
using RestSharp;

namespace RecipeApiApp.Core.Query {
    public sealed class RecipieProviderImpl : IRecipeProvider {
        private readonly IErrorWriter _errorWriter;
        private readonly IConfiguration _configuration;

        public RecipieProviderImpl (IErrorWriter errorWriter, IConfiguration configuration) {
            _errorWriter = errorWriter;
            _configuration = configuration;
        }

        public async Task<RecipePayload>
            GetRecipientsFromSearch (string searchTerm) {
                if (string.IsNullOrEmpty (searchTerm)) {
                    return MissingSearchTermResponse (searchTerm);
                }
                RestClient client = new RestClient ("https://api.edamam.com");

                string appId = _configuration.GetValue<string> ("RecipeApi_AppId");
                string appKey = _configuration.GetValue<string> ("RecipeApi_AppKey");

                // todo remove later
                _errorWriter.WriteString (appId + "   " + appKey);

                if (string.IsNullOrEmpty (appId)) {
                    throw new System.Exception ("AppId is null or empty");
                }
                if (string.IsNullOrEmpty (appKey)) {
                    throw new System.Exception ("AppKey is null or empty.");
                }

                RestRequest request = new RestRequest ("search", Method.GET);
                request.AddQueryParameter ("q", searchTerm);
                request.AddQueryParameter ("app_id", appId);
                request.AddQueryParameter ("app_key", appKey);

                try {

                    IRestResponse<RecipePayload> response =
                        await client.ExecuteAsync<RecipePayload> (request);

                    if (response.IsSuccessful) {
                        return response.Data;
                    }
                    return ErrorResponse (response, searchTerm);

                } catch (System.Exception ex) {
                    _errorWriter.WriteException (ex);
                    return RecipeErrorResponses.ExceptionResponse (ex, searchTerm);
                }
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