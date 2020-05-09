using System.Collections.Generic;

namespace RecipeApiApp.Core.Models
{
    public class RecipePayload
    {
        public string Q { get; set; }

        public int From { get; set; }

        public int To { get; set; }

        public bool More { get; set; }

        public int Count { get; set; }

        public List<Hit> Hits { get; set; }

        public string Warning { get; set; }
    }
}
