using System.Text.Json;

namespace RecipeApiApp.Tests.Utils
{
    public static class JsonCompare
    {
        public static bool Compare(this object obj, object another)
        {
            var objJson = JsonSerializer.Serialize(obj);
            var anotherJson = JsonSerializer.Serialize(another);

            return objJson == anotherJson;
        }
    }
}
