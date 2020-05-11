namespace RecipeApiApp.Core.Env {
    public interface IRuntimeEnvProvider {
        bool IsProdEnv ();
        bool IsDevEnv ();
    }
}