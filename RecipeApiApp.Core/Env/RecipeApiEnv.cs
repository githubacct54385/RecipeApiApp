using System;

namespace RecipeApiApp.Core.Env {

    public enum RuntimeSetting {
        Production = 0,
        Development = 1
    }
    public class RecipeApiEnv {
        private readonly IRuntimeEnvProvider _provider;
        public RecipeApiEnv (IRuntimeEnvProvider provider) {
            _provider = provider;
        }
        public RuntimeSetting GetSettings () {
            return _provider.GetRuntimeEnv ();
        }
    }
}