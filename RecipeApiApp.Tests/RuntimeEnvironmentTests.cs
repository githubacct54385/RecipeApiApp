using Autofac.Extras.Moq;
using RecipeApiApp.Core.Env;
using Xunit;

namespace RecipeApiApp.Tests {
    public class RuntimeEnvironmentTests {
        [Fact]
        public void GetSettings_ReturnsDevelopment_WhenMissingEnvironmentVariable () {

            RuntimeSetting expected = RuntimeSetting.Development;

            using (AutoMock mock = AutoMock.GetLoose ()) {
                mock.Mock<IRuntimeEnvProvider> ()
                    .Setup (x => x.GetRuntimeEnv ())
                    .Returns (expected);

                RecipeApiEnv sut = mock.Create<RecipeApiEnv> ();
                RuntimeSetting actual = sut.GetSettings ();

                Assert.Equal (expected, actual);
            }
        }
    }
}