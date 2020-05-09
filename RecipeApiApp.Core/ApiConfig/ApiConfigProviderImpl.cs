using System;
using System.IO;
using System.Text.Json;

namespace RecipeApiApp.Core.ApiConfig
{
    public sealed class ApiConfigProviderImpl : IApiConfigProvider
    {
        public ApiConfigProviderImpl()
        {
        }

        public ApiConfigSettings GetApiConfigSettings()
        {
            string path =
                AppDomain.CurrentDomain.BaseDirectory + "appSettings.json";

            ApiConfigSettings settings =
                JsonSerializer
                    .Deserialize<ApiConfigSettings>(File.ReadAllText(path));

            return settings;
        }
    }
}
