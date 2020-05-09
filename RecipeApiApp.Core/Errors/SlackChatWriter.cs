using System;
using RecipeApiApp.Core.ApiConfig;
using SlackWriter;

namespace RecipeApiApp.Core.Errors {
    public class SlackChatWriter : IErrorWriter {
        private readonly IApiConfigProvider _provider;

        public SlackChatWriter (IApiConfigProvider provider) {
            _provider = provider;
        }
        public void Write (Exception ex) {
            SlackClient slackClient = new SlackClient ();
            string slackSecret = _provider.GetApiConfigSettings ().SlackSecret;
            slackClient.WriteTextMessageToChannel (slackSecret, $"{ex.Message}\n\n{ex.StackTrace}");
        }
    }
}