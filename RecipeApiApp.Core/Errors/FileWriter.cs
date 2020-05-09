using System;
using System.Collections.Generic;
using System.IO;

namespace RecipeApiApp.Core.Errors {
    public class FileWriter : IErrorWriter {
        public void Write (Exception ex) {
            if (FileExistsForToday ()) {
                WriteToExistingFile (ex);
            } else {
                WriteToNewFile (ex);
            }
        }

        private bool FileExistsForToday () {
            return File.Exists (AppDomain.CurrentDomain.BaseDirectory + FileName ());
        }

        private void WriteToExistingFile (Exception ex) {
            List<string> contents = CreateExceptionContents (ex);
            File.AppendAllLines (AppDomain.CurrentDomain.BaseDirectory + FileName (), contents);
        }

        private void WriteToNewFile (Exception ex) {
            List<string> contents = CreateExceptionContents (ex);
            File.Create (AppDomain.CurrentDomain.BaseDirectory + FileName ());
            File.WriteAllLines (AppDomain.CurrentDomain.BaseDirectory + FileName (), contents);
        }

        private List<string> CreateExceptionContents (Exception ex) {
            List<string> contents = new List<string> () { $"Date: {DateTime.Now.ToString()}", $"Message: {ex.Message}", $"Stacktrace: {ex.StackTrace}" };
            return contents;
        }

        private string FileName () {
            return $"{DateTime.Now.ToString("yyyy-MM-dd")}" + "Log.txt";
        }
    }
}