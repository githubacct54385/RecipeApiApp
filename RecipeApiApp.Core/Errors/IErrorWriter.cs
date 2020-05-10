using System;

namespace RecipeApiApp.Core.Errors {
    public interface IErrorWriter {
        void WriteException (Exception ex);
        void WriteString (string s);
    }
}