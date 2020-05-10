using Microsoft.Extensions.Configuration;

namespace RecipeApiApp.Core.ApiConfig {
    public sealed class EnvironmentVarsConfigProviderImpl : IApiConfigProvider {
        private readonly IConfiguration _configuration;
        public EnvironmentVarsConfigProviderImpl (IConfiguration configuration) {
            _configuration = configuration;
        }
        public ApiConfigSettings GetApiConfigSettings () {
            string appId = "4e4ab3d3"; // _configuration.GetSection ("ApiConfigSettings") ["AppId"];
            string appKey = "b169836d4ddb0021bde0d16e53f5c83e"; //_configuration.GetSection ("ApiConfigSettings") ["AppKey"];
            string slackSecret = "T012WPQEDQ9/B013QHLHJ1X/muNNnnKFWaA2p7y9Ew2bhyCp"; //_configuration.GetSection ("ApiConfigSettings") ["SlackSecret"];

            return new ApiConfigSettings (appId, appKey, slackSecret);
        }
    }
}