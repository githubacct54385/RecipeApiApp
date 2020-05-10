using Microsoft.Extensions.Configuration;

namespace RecipeApiApp.Core.Env {
    public sealed class RuntimeEnvProviderImpl : IRuntimeEnvProvider {
        private IConfiguration _configuration;
        public RuntimeEnvProviderImpl (IConfiguration configuration) {
            _configuration = configuration;
        }
        public RuntimeSetting GetRuntimeEnv () {
            string runtimeEnv = _configuration["RecipeApi_Environment"];
            // string runtimeEnv = System.Environment.GetEnvironmentVariable ("RecipeApi_Environment");

            switch (runtimeEnv) {
                case "Production":
                    return RuntimeSetting.Production;
                default:
                    return RuntimeSetting.Development;
            }
        }
    }
}