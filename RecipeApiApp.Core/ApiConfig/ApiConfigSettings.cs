namespace RecipeApiApp.Core.ApiConfig {
    public class ApiConfigSettings {
        public ApiConfigSettings () { }

        public ApiConfigSettings (string appId, string appKey, string slackSecret) {
            AppId = appId;
            AppKey = appKey;
            SlackSecret = slackSecret;
        }

        public string AppId { get; set; }

        public string AppKey { get; set; }
        public string SlackSecret { get; set; }
    }
}