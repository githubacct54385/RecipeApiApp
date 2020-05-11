using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RecipeApiApp.Core.Models;

namespace RecipeApiApp.Core.Query {
    public class RecipeLookup {
        private readonly IRecipeProvider _provider;
        private readonly IConfiguration _config;

        public RecipeLookup (IRecipeProvider provider, IConfiguration config) {
            _provider = provider;
            _config = config;
        }

        public Task<RecipePayload> SearchRecipes (SearchParams searchParams) {
            return _provider.GetRecipientsFromSearch (searchParams);
        }
    }
}