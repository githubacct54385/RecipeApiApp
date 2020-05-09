using System;

namespace RecipeApiApp.Core.Errors {
    public interface IErrorWriter {
        void Write (Exception ex);
    }
}