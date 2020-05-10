using Microsoft.Extensions.Configuration;

namespace RecipeApiApp.Core.ApiConfig {
    public sealed class EnvironmentVarsConfigProviderImpl : IApiConfigProvider {
        private readonly IConfiguration _configuration;
        public EnvironmentVarsConfigProviderImpl (IConfiguration configuration) {
            _configuration = configuration;
        }
        public ApiConfigSettings GetApiConfigSettings () {
            // string appId = System.Environment.GetEnvironmentVariable ("RecipeApi_AppId");
            // string appKey = System.Environment.GetEnvironmentVariable ("RecipeApi_AppKey");
            // string slackSecret = System.Environment.GetEnvironmentVariable ("RecipeApi_SlackSecret");

            string appId = _configuration["RecipeApi_AppId"];
            string appKey = _configuration["RecipeApi_AppKey"];
            string slackSecret = _configuration["RecipeApi_SlackSecret"];

            return new ApiConfigSettings (appKey, appKey, slackSecret);
        }
    }
}