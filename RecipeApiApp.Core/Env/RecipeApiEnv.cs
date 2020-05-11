using System;

namespace RecipeApiApp.Core.Env {

    public enum RuntimeSetting {
        Production = 0,
        Development = 1,
        Neither = 2,
        Both = 3,
        Unknown = 4
    }
    public class RecipeApiEnv {
        private readonly IRuntimeEnvProvider _provider;
        public RecipeApiEnv (IRuntimeEnvProvider provider) {
            _provider = provider;
        }
        public RuntimeSetting GetSettings () {
            bool isDevEnv = _provider.IsDevEnv ();
            bool isProdEnv = _provider.IsProdEnv ();

            if (isDevEnv && !isProdEnv) return RuntimeSetting.Development;
            if (!isDevEnv && isProdEnv) return RuntimeSetting.Production;
            // cannot be both
            if (isDevEnv && isProdEnv) return RuntimeSetting.Both;
            // cannot be neither
            return RuntimeSetting.Neither;
        }
    }
}