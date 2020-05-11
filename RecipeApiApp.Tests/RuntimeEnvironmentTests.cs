using Autofac.Extras.Moq;
using RecipeApiApp.Core.Env;
using Xunit;

namespace RecipeApiApp.Tests {
    public class RuntimeEnvironmentTests {
        [Fact]
        public void GetSettings_ReturnsDevelopment_WhenIsDevAndNotProd () {

            RuntimeSetting expected = RuntimeSetting.Development;

            using (AutoMock mock = AutoMock.GetLoose ()) {
                mock.Mock<IRuntimeEnvProvider> ()
                    .Setup (x => x.IsDevEnv ())
                    .Returns (true);
                mock.Mock<IRuntimeEnvProvider> ()
                    .Setup (x => x.IsProdEnv ())
                    .Returns (false);

                RecipeApiEnv sut = mock.Create<RecipeApiEnv> ();
                RuntimeSetting actual = sut.GetSettings ();

                Assert.Equal (expected, actual);
            }
        }

        [Fact]
        public void GetSettings_ReturnsProduction_WhenIsDevAndNotProd () {

            RuntimeSetting expected = RuntimeSetting.Production;

            using (AutoMock mock = AutoMock.GetLoose ()) {
                mock.Mock<IRuntimeEnvProvider> ()
                    .Setup (x => x.IsDevEnv ())
                    .Returns (false);
                mock.Mock<IRuntimeEnvProvider> ()
                    .Setup (x => x.IsProdEnv ())
                    .Returns (true);

                RecipeApiEnv sut = mock.Create<RecipeApiEnv> ();
                RuntimeSetting actual = sut.GetSettings ();

                Assert.Equal (expected, actual);
            }
        }

        [Fact]
        public void GetSettings_ReturnsBoth_WhenIsDevAndIsProd () {

            RuntimeSetting expected = RuntimeSetting.Both;

            using (AutoMock mock = AutoMock.GetLoose ()) {
                mock.Mock<IRuntimeEnvProvider> ()
                    .Setup (x => x.IsDevEnv ())
                    .Returns (true);
                mock.Mock<IRuntimeEnvProvider> ()
                    .Setup (x => x.IsProdEnv ())
                    .Returns (true);

                RecipeApiEnv sut = mock.Create<RecipeApiEnv> ();
                RuntimeSetting actual = sut.GetSettings ();

                Assert.Equal (expected, actual);
            }
        }

        [Fact]
        public void GetSettings_ReturnsNeither_WhenNotIsDevAndNotIsProd () {

            RuntimeSetting expected = RuntimeSetting.Neither;

            using (AutoMock mock = AutoMock.GetLoose ()) {
                mock.Mock<IRuntimeEnvProvider> ()
                    .Setup (x => x.IsDevEnv ())
                    .Returns (false);
                mock.Mock<IRuntimeEnvProvider> ()
                    .Setup (x => x.IsProdEnv ())
                    .Returns (false);

                RecipeApiEnv sut = mock.Create<RecipeApiEnv> ();
                RuntimeSetting actual = sut.GetSettings ();

                Assert.Equal (expected, actual);
            }
        }
    }
}