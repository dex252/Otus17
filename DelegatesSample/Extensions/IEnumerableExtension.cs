using System.Collections;
using System.Diagnostics;

namespace DelegatesSample.Extensions
{
    public static class IEnumerableExtension
    {
        public static T GetMax<T>(this IEnumerable e, Func<T, float> getParameter)
            where T : class
        {
            var dictionary = new Dictionary<T, float>();
            foreach (var item in e)
            {
                var key = item as T;
                dictionary.TryAdd(key, getParameter(key));
            }

            if (!dictionary.Any())
            {
                Debug.WriteLine("Коллекция пустая, максимальный элемент равен null");
                return null;
            }

            var max = dictionary.MaxBy(e => e.Value);
            Debug.WriteLine($"Максимальный элемент найден, числовое значение элемента равно {max.Value}");
            return max.Key;
        }
    }
}
