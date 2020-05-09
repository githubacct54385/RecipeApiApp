namespace RecipeApiApp.Core.ApiConfig
{
    public class ApiConfigRepositoryImpl : IApiConfigRepository
    {
        private readonly IApiConfigProvider _provider;

        public ApiConfigRepositoryImpl(IApiConfigProvider provider)
        {
            _provider = provider;
        }

        public ApiConfigSettings GetSettings()
        {
            return _provider.GetApiConfigSettings();
        }
    }
}
