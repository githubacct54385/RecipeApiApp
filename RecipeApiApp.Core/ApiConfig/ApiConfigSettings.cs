namespace RecipeApiApp.Core.ApiConfig
{
    public class ApiConfigSettings
    {
        public ApiConfigSettings(string appId, string appKey)
        {
            AppId = appId;
            AppKey = appKey;
        }

        public string AppId { get; set; }

        public string AppKey { get; set; }
    }
}
