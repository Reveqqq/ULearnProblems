using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews;

public static class ExtensionsTask
{
    /// <summary>
    /// Медиана списка из нечетного количества элементов — это серединный элемент списка после сортировки.
    /// Медиана списка из четного количества элементов — это среднее арифметическое 
    /// двух серединных элементов списка после сортировки.
    /// </summary>
    /// <exception cref="InvalidOperationException">Если последовательность не содержит элементов</exception>
    public static double Median(this IEnumerable<double> items)
    {
        var itemsList = items.ToList();
        if (itemsList.Count() == 0)
            throw new InvalidOperationException();
        itemsList = itemsList.OrderBy(item => item).ToList();
        var count = itemsList.Count();
        var medianIndex = count / 2;
        if (count % 2 != 0)
            return itemsList.ElementAt(medianIndex);
        else
            return (itemsList.ElementAt(medianIndex) + itemsList.ElementAt(medianIndex - 1)) / 2;
    }

    /// <returns>
    /// Возвращает последовательность, состоящую из пар соседних элементов.
    /// Например, по последовательности {1,2,3} метод должен вернуть две пары: (1,2) и (2,3).
    /// </returns>
    public static IEnumerable<(T First, T Second)> Bigrams<T>(this IEnumerable<T> items)
    {
        T first = default, second = default;
        bool b1 = false, b2 = false;
        foreach (var item in items)
        {
            if (!b2)
                b2 = true;
            else if (!b1)
                b1 = true;
            first = second;
            second = item;
            if (b1)
                yield return new(first, second);
        }
    }
}