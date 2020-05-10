namespace RecipeApiApp.Core.Env {
    public interface IRuntimeEnvProvider {
        RuntimeSetting GetRuntimeEnv ();
    }
}