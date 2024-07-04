using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            var days = new string[31];
            for (var y = 0; y < days.Length; y++)
                days[y] = (y + 1).ToString();
            var birthsCounts = new double[days.Length];
            foreach (var item in names)
                if (item.BirthDate.Day != 1 && item.Name == name) birthsCounts[item.BirthDate.Day - 1]++;
            return new HistogramData(
                string.Format("Рождаемость людей с именем '{0}'", name),
                days,
                birthsCounts);
        }
    }
}