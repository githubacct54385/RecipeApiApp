using Microsoft.Extensions.Configuration;

namespace RecipeApiApp.Core.ApiConfig {
    public sealed class EnvironmentVarsConfigProviderImpl : IApiConfigProvider {
        private readonly IConfiguration _configuration;
        public EnvironmentVarsConfigProviderImpl (IConfiguration configuration) {
            _configuration = configuration;
        }
        public ApiConfigSettings GetApiConfigSettings () {
            string appId = _configuration.GetValue<string> ("RecipeApiApp_AppId");
            string appKey = _configuration.GetValue<string> ("RecipeApiApp_AppKey");
            string slackSecret = _configuration.GetValue<string> ("RecipeApiApp_SlackSecret");

            return new ApiConfigSettings (appId, appKey, slackSecret);
        }
    }
}