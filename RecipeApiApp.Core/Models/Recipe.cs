using System.Collections.Generic;

namespace RecipeApiApp.Core.Models {
    public class Recipe {
        public string Uri { get; set; }

        public string Label { get; set; }

        public string Image { get; set; }

        public string Source { get; set; }

        public string Url { get; set; }

        public string ShareAs { get; set; }

        public double Yield { get; set; }

        public List<string> DietLabels { get; set; }

        public List<string> HealthLabels { get; set; }

        public List<string> Cautions { get; set; }

        public List<string> IngredientLines { get; set; }

        public List<Ingredient> Ingredients { get; set; }
        public double Calories { get; set; }
        public double TotalWeight { get; set; }
        public double TotalTime { get; set; }
    }
}