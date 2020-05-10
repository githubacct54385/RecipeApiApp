namespace RecipeApiApp.Core.Env {
    public sealed class RuntimeEnvProviderImpl : IRuntimeEnvProvider {
        public RuntimeSetting GetRuntimeEnv () {
            string runtimeEnv = System.Environment.GetEnvironmentVariable ("RecipeApi_Environment");

            switch (runtimeEnv) {
                case "Production":
                    return RuntimeSetting.Production;
                default:
                    return RuntimeSetting.Development;
            }
        }
    }
}