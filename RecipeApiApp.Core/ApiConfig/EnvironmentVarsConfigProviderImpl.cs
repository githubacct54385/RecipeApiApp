using Microsoft.Extensions.Configuration;

namespace RecipeApiApp.Core.ApiConfig {
    public sealed class EnvironmentVarsConfigProviderImpl : IApiConfigProvider {
        private readonly IConfiguration _configuration;
        public EnvironmentVarsConfigProviderImpl (IConfiguration configuration) {
            _configuration = configuration;
        }
        public ApiConfigSettings GetApiConfigSettings () {
            string appId = _configuration.GetSection ("ApiConfigSettings") ["AppId"];
            string appKey = _configuration.GetSection ("ApiConfigSettings") ["AppKey"];
            string slackSecret = _configuration.GetSection ("ApiConfigSettings") ["SlackSecret"];

            return new ApiConfigSettings (appKey, appKey, slackSecret);
        }
    }
}