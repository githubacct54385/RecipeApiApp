using System;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace RecipeApiApp.Core.ApiConfig {
    public sealed class ApiConfigProviderImpl : IApiConfigProvider {
        private readonly IConfiguration _configuration;
        public ApiConfigProviderImpl (IConfiguration configuration) {
            _configuration = configuration;
        }

        public ApiConfigSettings GetApiConfigSettings () {

            string appId = _configuration["ApiConfigSettings:AppId"];
            string appKey = _configuration["ApiConfigSettings:AppKey"];
            string slackSecret = _configuration["ApiConfigSettings:SlackSecret"];

            return new ApiConfigSettings (appId, appKey, slackSecret);
        }
    }
}