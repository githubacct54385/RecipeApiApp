using System.Threading.Tasks;
using RecipeApiApp.Core.Models;

namespace RecipeApiApp.Core.Query
{
    public interface IRecipeProvider
    {
        Task<RecipePayload> GetRecipientsFromSearch(string searchTerm);
    }
}
