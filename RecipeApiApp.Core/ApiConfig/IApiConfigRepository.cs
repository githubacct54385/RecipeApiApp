namespace RecipeApiApp.Core.ApiConfig
{
    public interface IApiConfigRepository
    {
        ApiConfigSettings GetSettings();
    }
}
