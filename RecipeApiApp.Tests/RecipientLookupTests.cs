using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using RecipeApiApp.Core.ApiConfig;
using RecipeApiApp.Core.Models;
using RecipeApiApp.Core.Query;
using RecipeApiApp.Tests.Utils;
using Xunit;

namespace RecipeApiApp.Tests {
    public class RecipientLookupTests {
        [Fact]
        public async Task RecipeLookup_WithSearchQuery_ReturnsCorrectListAsync () {
            List<Hit> hits = SampleData.SampleHits ();

            const string searchTerm = "Chicken";

            RecipePayload expected = new RecipePayload ();
            expected.Q = searchTerm;
            expected.From = 0;
            expected.To = 10;
            expected.More = true;
            expected.Count = 100000;
            expected.Warning = "";
            expected.Hits = hits;

            SearchParams searchParams = new SearchParams (searchTerm, 0, 10);

            using (AutoMock mock = AutoMock.GetLoose ()) {
                mock.Mock<IApiConfigProvider> ()
                    .Setup (x => x.GetApiConfigSettings ())
                    .Returns (new ApiConfigSettings ("123", "abc", "hello"));

                mock.Mock<IRecipeSearchProvider> ()
                    .Setup (x => x.RunSearch (searchParams, "123", "abc"))
                    .Returns (Task.FromResult (expected));

                RecipeProviderImpl sut = mock.Create<RecipeProviderImpl> ();
                RecipePayload actual = await sut.GetRecipientsFromSearch (searchParams);

                Assert.True (JsonCompare.Compare (expected, actual));
            }
        }

        [Theory]
        [InlineData ("")]
        [InlineData (null)]
        public async Task RecipeLookup_WithEmptyStringSearchQuery_ReturnsNoRowsAndWarningAsync (
            string searchTerm
        ) {
            RecipePayload expected = new RecipePayload ();
            expected.Q = searchTerm;
            expected.From = 0;
            expected.To = 10;
            expected.More = false;
            expected.Count = 0;
            expected.Warning = "Search term cannot be empty or null.";
            expected.Hits = new List<Hit> ();

            SearchParams searchParams = new SearchParams (searchTerm, 0, 10);

            using (AutoMock mock = AutoMock.GetLoose ()) {
                mock.Mock<IApiConfigProvider> ()
                    .Setup (x => x.GetApiConfigSettings ())
                    .Returns (new ApiConfigSettings ("123", "abc", "hello"));

                mock.Mock<IRecipeSearchProvider> ()
                    .Setup (x => x.RunSearch (searchParams, "123", "abc"))
                    .Returns (Task.FromResult (expected));

                RecipeProviderImpl sut = mock.Create<RecipeProviderImpl> ();
                RecipePayload actual = await sut.GetRecipientsFromSearch (searchParams);

                Assert.True (JsonCompare.Compare (expected, actual));
            }
        }

        [Fact]
        public async Task RecipeLookup_WithNegativeFromParameter_ReturnsNoRowsAndWarningAsync () {

            RecipePayload expected = new RecipePayload ();
            expected.Q = "chicken";
            expected.From = 0;
            expected.To = 10;
            expected.More = false;
            expected.Count = 0;
            expected.Warning = "Parameter From is less than zero.  Please provide a value greater than or equal to zero and less than the To parameter.";
            expected.Hits = new List<Hit> ();

            SearchParams searchParams = new SearchParams ("chicken", -1, 10);

            using (AutoMock mock = AutoMock.GetLoose ()) {
                mock.Mock<IApiConfigProvider> ()
                    .Setup (x => x.GetApiConfigSettings ())
                    .Returns (new ApiConfigSettings ("123", "abc", "hello"));

                mock.Mock<IRecipeSearchProvider> ()
                    .Setup (x => x.RunSearch (searchParams, "123", "abc"))
                    .Returns (Task.FromResult (expected));

                RecipeProviderImpl sut = mock.Create<RecipeProviderImpl> ();
                RecipePayload actual = await sut.GetRecipientsFromSearch (searchParams);

                Assert.True (JsonCompare.Compare (expected, actual));
            }
        }

        [Fact]
        public async Task RecipeLookup_WithNegativeToParameter_ReturnsNoRowsAndWarningAsync () {
            RecipePayload expected = new RecipePayload ();
            expected.Q = "chicken";
            expected.From = 0;
            expected.To = 10;
            expected.More = false;
            expected.Count = 0;
            expected.Warning = "Parameter To is less than zero.  Please provide a value greater than the To parameter.";
            expected.Hits = new List<Hit> ();

            SearchParams searchParams = new SearchParams ("chicken", 0, -10);

            using (AutoMock mock = AutoMock.GetLoose ()) {
                mock.Mock<IApiConfigProvider> ()
                    .Setup (x => x.GetApiConfigSettings ())
                    .Returns (new ApiConfigSettings ("123", "abc", "hello"));

                mock.Mock<IRecipeSearchProvider> ()
                    .Setup (x => x.RunSearch (searchParams, "123", "abc"))
                    .Returns (Task.FromResult (expected));

                RecipeProviderImpl sut = mock.Create<RecipeProviderImpl> ();
                RecipePayload actual = await sut.GetRecipientsFromSearch (searchParams);

                Assert.True (JsonCompare.Compare (expected, actual));
            }
        }

        [Fact]
        public async Task RecipeLookup_WithFromLessThanTo_ReturnsNoRowsAndWarningAsync () {
            RecipePayload expected = new RecipePayload ();
            expected.Q = "chicken";
            expected.From = 0;
            expected.To = 10;
            expected.More = false;
            expected.Count = 0;
            expected.Warning = "Parameter From is greater than or equal to parameter To.  Please provide a From parameter that is less that the To parameter.";
            expected.Hits = new List<Hit> ();

            SearchParams searchParams = new SearchParams ("chicken", 10, 5);

            using (AutoMock mock = AutoMock.GetLoose ()) {
                mock.Mock<IApiConfigProvider> ()
                    .Setup (x => x.GetApiConfigSettings ())
                    .Returns (new ApiConfigSettings ("123", "abc", "hello"));

                mock.Mock<IRecipeSearchProvider> ()
                    .Setup (x => x.RunSearch (searchParams, "123", "abc"))
                    .Returns (Task.FromResult (expected));

                RecipeProviderImpl sut = mock.Create<RecipeProviderImpl> ();
                RecipePayload actual = await sut.GetRecipientsFromSearch (searchParams);

                Assert.True (JsonCompare.Compare (expected, actual));
            }
        }

        [Theory]
        [InlineData (null)]
        [InlineData ("")]
        public async Task RecipeLookup_WithMissingAppId_ReturnsNoRowsAndWarningAsync (string appId) {
            RecipePayload expected = new RecipePayload ();
            expected.Q = "chicken";
            expected.From = 0;
            expected.To = 10;
            expected.More = false;
            expected.Count = 0;
            expected.Warning = "Configuration Error: Missing AppId.";
            expected.Hits = new List<Hit> ();

            SearchParams searchParams = new SearchParams ("chicken", 0, 10);

            using (AutoMock mock = AutoMock.GetLoose ()) {
                mock.Mock<IApiConfigProvider> ()
                    .Setup (x => x.GetApiConfigSettings ())
                    .Returns (new ApiConfigSettings (appId, "abc", "hello"));

                mock.Mock<IRecipeSearchProvider> ()
                    .Setup (x => x.RunSearch (searchParams, appId, "abc"))
                    .Returns (Task.FromResult (expected));

                RecipeProviderImpl sut = mock.Create<RecipeProviderImpl> ();
                RecipePayload actual = await sut.GetRecipientsFromSearch (searchParams);

                Assert.True (JsonCompare.Compare (expected, actual));
            }
        }

        [Theory]
        [InlineData (null)]
        [InlineData ("")]
        public async Task RecipeLookup_MissingAppKey_ReturnsNoRowsAndWarningAsync (string appKey) {
            RecipePayload expected = new RecipePayload ();
            expected.Q = "chicken";
            expected.From = 0;
            expected.To = 10;
            expected.More = false;
            expected.Count = 0;
            expected.Warning = "Configuration Error: Missing AppKey.";
            expected.Hits = new List<Hit> ();

            SearchParams searchParams = new SearchParams ("chicken", 0, 10);

            using (AutoMock mock = AutoMock.GetLoose ()) {
                mock.Mock<IApiConfigProvider> ()
                    .Setup (x => x.GetApiConfigSettings ())
                    .Returns (new ApiConfigSettings ("123", appKey, "hello"));

                mock.Mock<IRecipeSearchProvider> ()
                    .Setup (x => x.RunSearch (searchParams, "123", appKey))
                    .Returns (Task.FromResult (expected));

                RecipeProviderImpl sut = mock.Create<RecipeProviderImpl> ();
                RecipePayload actual = await sut.GetRecipientsFromSearch (searchParams);

                Assert.True (JsonCompare.Compare (expected, actual));
            }
        }

        [Theory]
        [InlineData (null)]
        [InlineData ("")]
        public async Task RecipeLookup_MissingSlackSecret_ReturnsNoRowsAndWarningAsync (string slackSecret) {
            RecipePayload expected = new RecipePayload ();
            expected.Q = "chicken";
            expected.From = 0;
            expected.To = 10;
            expected.More = false;
            expected.Count = 0;
            expected.Warning = "Configuration Error: Missing SlackSecret.";
            expected.Hits = new List<Hit> ();

            SearchParams searchParams = new SearchParams ("chicken", 0, 10);

            using (AutoMock mock = AutoMock.GetLoose ()) {
                mock.Mock<IApiConfigProvider> ()
                    .Setup (x => x.GetApiConfigSettings ())
                    .Returns (new ApiConfigSettings ("123", "abc", slackSecret));

                mock.Mock<IRecipeSearchProvider> ()
                    .Setup (x => x.RunSearch (searchParams, "123", "abc"))
                    .Returns (Task.FromResult (expected));

                RecipeProviderImpl sut = mock.Create<RecipeProviderImpl> ();
                RecipePayload actual = await sut.GetRecipientsFromSearch (searchParams);

                Assert.True (JsonCompare.Compare (expected, actual));
            }
        }
    }
}