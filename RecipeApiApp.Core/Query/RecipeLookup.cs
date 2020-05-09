using System.Threading.Tasks;
using RecipeApiApp.Core.Models;

namespace RecipeApiApp.Core.Query
{
    public class RecipeLookup
    {
        private readonly IRecipeProvider _provider;

        public RecipeLookup(IRecipeProvider provider)
        {
            _provider = provider;
        }

        public Task<RecipePayload> SearchRecipes(string searchTerm)
        {
            return _provider.GetRecipientsFromSearch(searchTerm);
        }
    }
}
