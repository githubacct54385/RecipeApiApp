using System;
using System.Text;

namespace RecipeApiApp.Core.Errors {
    public sealed class AzureTraceWriter : IErrorWriter {
        public void WriteException (Exception ex) {
            StringBuilder sb = new StringBuilder ();
            sb.AppendLine ($"Message: {ex.Message}");
            sb.AppendLine ($"StackTrace: {ex.StackTrace}");
            System.Diagnostics.Trace.TraceError (sb.ToString ());
        }

        public void WriteString (string s) {
            System.Diagnostics.Trace.TraceError (s);
        }
    }
}