using Autofac.Extras.Moq;
using RecipeApiApp.Core.ApiConfig;
using Xunit;

namespace RecipeApiApp.Tests {
    public class GetApiConfigRepositoryTests {
        [Fact]
        public void GetSettings_ReturnsCorrectAppIdAndAppKey () {
            ApiConfigSettings sampleApiSettings =
                new ApiConfigSettings ("123", "abc", "blahblah");

            using (AutoMock mock = AutoMock.GetLoose ()) {
                mock
                    .Mock<IApiConfigProvider> ()
                    .Setup (x => x.GetApiConfigSettings ())
                    .Returns (sampleApiSettings);

                ApiConfigRepositoryImpl sut =
                    mock.Create<ApiConfigRepositoryImpl> ();
                ApiConfigSettings actual = sut.GetSettings ();

                Assert.Equal ("123", actual.AppId);
                Assert.Equal ("abc", actual.AppKey);
                Assert.Equal ("blahblah", actual.SlackSecret);
            }
        }
    }
}