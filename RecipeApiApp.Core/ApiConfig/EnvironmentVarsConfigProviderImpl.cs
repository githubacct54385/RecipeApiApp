namespace RecipeApiApp.Core.ApiConfig {
    public sealed class EnvironmentVarsConfigProviderImpl : IApiConfigProvider {
        public ApiConfigSettings GetApiConfigSettings () {
            string appId = System.Environment.GetEnvironmentVariable ("RecipeApi_AppId");
            string appKey = System.Environment.GetEnvironmentVariable ("RecipeApi_AppKey");

            return new ApiConfigSettings (appKey, appKey);
        }
    }
}