using System.Collections.Generic;
using RecipeApiApp.Core.Models;

namespace RecipeApiApp.Tests
{
    public class SampleData
    {
        public static List<Hit> SampleHits()
        {
            Recipe recipe = new Recipe();
            recipe.Uri =
                "http://www.edamam.com/ontologies/edamam.owl#recipe_b79327d05b8e5b838ad6cfd9576b30b6";
            recipe.Url =
                "http://www.seriouseats.com/recipes/2011/12/chicken-vesuvio-recipe.html";
            recipe.Image =
                "https://www.edamam.com/web-img/e42/e42f9119813e890af34c259785ae1cfb.jpg";
            recipe.ShareAs =
                "http://www.edamam.com/recipe/chicken-vesuvio-b79327d05b8e5b838ad6cfd9576b30b6/chicken";
            recipe.Label = "Chicken Vesuvio";
            recipe.Source = "Serious Eats";
            recipe.Yield = 4.0;
            recipe.DietLabels = new List<string>() { "Low-carb" };
            recipe.HealthLabels =
                new List<string>() { "Sugar-Conscious", "Peanut-Free" };
            recipe.Cautions = new List<string>() { "Sulfites" };
            recipe.IngredientLines =
                new List<string>()
                { "1/2 cup olive oil", "5 cloves garlic, peeled" };

            recipe.Ingredients =
                new List<Ingredient>()
                {
                    new Ingredient()
                    { Text = "1/2 cup olive oil", Weight = 108.0 },
                    new Ingredient()
                    { Text = "5 cloves garlic, peeled", Weight = 15.0 }
                };

            Hit hit = new Hit();
            hit.Bookmarked = false;
            hit.Bought = false;
            hit.Recipe = recipe;

            List<Hit> output = new List<Hit>();
            output.Add (hit);

            return output;
        }
    }
}
