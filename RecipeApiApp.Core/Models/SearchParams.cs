namespace RecipeApiApp.Core.Models {
    public class SearchParams {
        public SearchParams (string searchTerm, int from, int to) {
            SearchTerm = searchTerm;
            From = from;
            To = to;
        }

        public string SearchTerm { get; set; }
        public int From { get; set; }
        public int To { get; set; }
    }
}