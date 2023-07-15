using System.Collections;

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
                return null;
            }

            var max = dictionary.MaxBy(e => e.Value);
            return max.Key;
        }
    }
}
