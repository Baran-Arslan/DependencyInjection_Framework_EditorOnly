using System.Collections.Generic;

namespace iCare.Core.Extensions {
    public static class EnumerableExtensions {
        public static List<T> ToList<T>(this IEnumerable<T> source) {
            return new List<T>(source);
        }
    }
}