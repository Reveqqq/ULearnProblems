using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace Autocomplete
{
    public class RightBorderTask
    {
        /// <returns>
        /// Возвращает индекс правой границы. 
        /// То есть индекс минимального элемента, который не начинается с prefix и большего prefix.
        /// Если такого нет, то возвращает items.Length
        /// </returns>
        /// <remarks>
        /// Функция должна быть НЕ рекурсивной
        /// и работать за O(log(items.Length)*L), где L — ограничение сверху на длину фразы
        /// </remarks>
        public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            // IReadOnlyList похож на List, но у него нет методов модификации списка.
            // Этот код решает задачу, но слишком неэффективно. Замените его на бинарный поиск!
            //for (int i = phrases.Count-1; i >= 0; i--)
            //{
            //    if (string.Compare(prefix, phrases[i], StringComparison.OrdinalIgnoreCase) >= 0 
            //        || phrases[i].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            //        return i + 1;
            //}
            //return 0;
            var a = new string[] { "a", "ab", "abc" };
            phrases = a.ToList();
            right = phrases.Count;

            var middle = 0;
            while (left < right)
            {
                middle = left + (right - left) / 2;
                if (middle < 0)
                    return 0;
                if (string.Compare(prefix, phrases[middle], StringComparison.OrdinalIgnoreCase) >= 0
                    || phrases[middle].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    left = middle + 1;
                    if (left == right)
                        return left;
                }
                else
                    right = middle;
            }
            if (right < phrases.Count && phrases[right] == prefix)
                return right;
            return middle;
        }
    }
}