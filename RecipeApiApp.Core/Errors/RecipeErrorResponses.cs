using System;
using System.Collections.Generic;
using RecipeApiApp.Core.Models;

namespace RecipeApiApp.Core.Errors {
    public class RecipeErrorResponses {
        public static RecipePayload ExceptionResponse (Exception ex, string searchTerm) {
            RecipePayload errorPayload = new RecipePayload ();
            errorPayload.Count = 0;
            errorPayload.Q = searchTerm;
            errorPayload.From = 0;
            errorPayload.To = 10;
            errorPayload.Warning = ex.Message;
            errorPayload.Hits = new List<Hit> ();
            return errorPayload;
        }
    }
}