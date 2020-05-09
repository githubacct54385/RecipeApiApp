using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using RecipeApiApp.Core.Models;
using RecipeApiApp.Core.Query;
using RecipeApiApp.Tests.Utils;
using Xunit;

namespace RecipeApiApp.Tests
{
    public class RecipientLookupTests
    {
        [Fact]
        public async Task RecipeLookup_WithSearchQuery_ReturnsCorrectListAsync()
        {
            List<Hit> hits = SampleData.SampleHits();
            const string searchTerm = "Chicken";

            RecipePayload expected = new RecipePayload();
            expected.Q = "Chicken";
            expected.From = 0;
            expected.To = 10;
            expected.More = true;
            expected.Count = 100000;
            expected.Warning = "";
            expected.Hits = hits;

            using (AutoMock mock = AutoMock.GetLoose())
            {
                mock
                    .Mock<IRecipeProvider>()
                    .Setup(x => x.GetRecipientsFromSearch(searchTerm))
                    .Returns(Task.FromResult(expected));

                RecipeLookup sut = mock.Create<RecipeLookup>();
                RecipePayload actual = await sut.SearchRecipes(searchTerm);

                Assert.True(JsonCompare.Compare(expected, actual));
            }
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async System.Threading.Tasks.Task
        RecipeLookup_WithEmptyStringSearchQuery_ReturnsNoRowsAndWarningAsync(
            string searchTerm
        )
        {
            RecipePayload expected = new RecipePayload();
            expected.Q = "";
            expected.From = 0;
            expected.To = 10;
            expected.More = false;
            expected.Count = 0;
            expected.Warning = "Please specify a search term.";
            expected.Hits = new List<Hit>();

            using (AutoMock mock = AutoMock.GetLoose())
            {
                mock
                    .Mock<IRecipeProvider>()
                    .Setup(x => x.GetRecipientsFromSearch(searchTerm))
                    .Returns(Task.FromResult(expected));

                RecipeLookup sut = mock.Create<RecipeLookup>();
                RecipePayload actual = await sut.SearchRecipes(searchTerm);

                Assert.True(JsonCompare.Compare(expected, actual));
            }
        }
    }
}
