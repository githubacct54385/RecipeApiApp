using System.Threading.Tasks;
using RecipeApiApp.Core.Models;

namespace RecipeApiApp.Core.Query {
    public interface IRecipeSearchHandler {
        Task<RecipePayload> Search (string searchTerm);
    }
}