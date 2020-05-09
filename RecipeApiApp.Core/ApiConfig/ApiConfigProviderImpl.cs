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
                AppDomain.CurrentDomain.BaseDirectory +
                "apiConfigSettings.json";

            string json = File.ReadAllText(path);
            ApiConfigSettings settings =
                JsonSerializer.Deserialize<ApiConfigSettings>(json);

            return settings;
        }
    }
}
