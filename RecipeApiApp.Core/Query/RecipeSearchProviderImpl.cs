using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeApiApp.Core.Errors;
using RecipeApiApp.Core.Models;
using RestSharp;

namespace RecipeApiApp.Core.Query {
    public sealed class RecipeSearchProviderImpl : IRecipeSearchProvider {
        private readonly IErrorWriter _errorWriter;
        public RecipeSearchProviderImpl (IErrorWriter errorWriter) {
            _errorWriter = errorWriter;
        }
        public async Task<RecipePayload> RunSearch (SearchParams searchParams, string appId, string appKey) {
            RestClient client = new RestClient ("https://api.edamam.com");
            RestRequest request = new RestRequest ("search", Method.GET);
            request.AddQueryParameter ("q", searchParams.SearchTerm);
            request.AddQueryParameter ("app_id", appId);
            request.AddQueryParameter ("app_key", appKey);
            request.AddQueryParameter ("from", searchParams.From.ToString ());
            request.AddQueryParameter ("to", searchParams.To.ToString ());

            try {
                IRestResponse<RecipePayload> response = await client.ExecuteAsync<RecipePayload> (request);
                if (response.IsSuccessful) {
                    response.Data.Warning = "";
                    return response.Data;
                }
                return FailureRequest (response.ErrorMessage, searchParams);

            } catch (System.Exception ex) {
                _errorWriter.WriteException (ex);
                return RecipeErrorResponses.ExceptionResponse (ex, searchParams.SearchTerm);
            }
        }

        private RecipePayload FailureRequest (string errorMessage, SearchParams searchParams) {
            RecipePayload payload = new RecipePayload ();
            payload.Q = searchParams.SearchTerm;
            payload.Count = 0;
            payload.More = false;
            payload.Warning = errorMessage;
            payload.Hits = new List<Hit> ();
            payload.From = searchParams.From;
            payload.To = searchParams.To;
            return payload;
        }
    }
}