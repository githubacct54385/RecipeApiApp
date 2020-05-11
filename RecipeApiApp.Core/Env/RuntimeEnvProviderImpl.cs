using Microsoft.Extensions.Configuration;

namespace RecipeApiApp.Core.Env {
    public sealed class RuntimeEnvProviderImpl : IRuntimeEnvProvider {
        private IConfiguration _configuration;
        public RuntimeEnvProviderImpl (IConfiguration configuration) {
            _configuration = configuration;
        }

        public bool IsDevEnv () {
            bool localEnv = _configuration.GetValue<bool> ("RecipeApiAppLocalEnv");
            return localEnv;
        }

        public bool IsProdEnv () {
            string runtimeEnv = _configuration.GetValue<string> ("RecipeApiApp_Environment");
            return runtimeEnv == "Production";
        }
    }
}