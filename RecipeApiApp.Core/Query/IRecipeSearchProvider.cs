using System.Threading.Tasks;
using RecipeApiApp.Core.Models;

namespace RecipeApiApp.Core.Query {
    public interface IRecipeSearchProvider {
        Task<RecipePayload> RunSearch (SearchParams searchParams, string appId, string appKey);
    }
}