namespace RecipeApiApp.Core.Models
{
    public class Hit
    {
        public bool Bookmarked { get; set; }

        public bool Bought { get; set; }

        public Recipe Recipe { get; set; }
    }
}
