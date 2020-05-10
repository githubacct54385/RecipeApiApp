using System;
using System.Collections.Generic;
using System.IO;

namespace RecipeApiApp.Core.Errors {
    public class FileWriter : IErrorWriter {
        public void WriteException (Exception ex) {
            if (FileExistsForToday ()) {
                WriteExceptionToExistingFile (ex);
            } else {
                WriteExceptionToNewFile (ex);
            }
        }

        public void WriteString (string s) {
            if (FileExistsForToday ()) {
                WriteStringToExistingFile (s);
            } else {
                WriteStringToNewFile (s);
            }
        }

        private bool FileExistsForToday () {
            return File.Exists (AppDomain.CurrentDomain.BaseDirectory + FileName ());
        }

        private void WriteStringToExistingFile (string s) {
            List<string> contents = new List<string> () { s };
            File.AppendAllLines (AppDomain.CurrentDomain.BaseDirectory + FileName (), contents);
        }
        private void WriteExceptionToExistingFile (Exception ex) {
            List<string> contents = CreateExceptionContents (ex);
            File.AppendAllLines (AppDomain.CurrentDomain.BaseDirectory + FileName (), contents);
        }

        private void WriteStringToNewFile (string s) {
            List<string> contents = new List<string> () { s };
            File.Create (AppDomain.CurrentDomain.BaseDirectory + FileName ());
            File.WriteAllLines (AppDomain.CurrentDomain.BaseDirectory + FileName (), contents);
        }
        private void WriteExceptionToNewFile (Exception ex) {
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