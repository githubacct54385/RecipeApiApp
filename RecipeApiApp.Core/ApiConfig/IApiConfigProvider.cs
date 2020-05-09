namespace RecipeApiApp.Core.ApiConfig {
    public interface IApiConfigProvider {
        ApiConfigSettings GetApiConfigSettings ();
    }
}