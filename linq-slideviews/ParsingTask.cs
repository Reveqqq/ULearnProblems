using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace linq_slideviews;

public class ParsingTask
{
    /// <param name="lines">все строки файла, которые нужно распарсить. Первая строка заголовочная.</param>
    /// <returns>Словарь: ключ — идентификатор слайда, значение — информация о слайде</returns>
    /// <remarks>Метод должен пропускать некорректные строки, игнорируя их</remarks>
    public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
    {
        return lines.Skip(1)
            .Select(line => Regex.Split(line, ";"))
            .Select(lineData =>
            {
                if (lineData.Length == 3)
                {
                    lineData[1] = lineData[1].Length >= 2 ?
                    lineData[1].Substring(0, 1).ToUpper() + lineData[1].Substring(1)
                    : lineData[1];
                    if (int.TryParse(lineData[0], out var id)
                    && Enum.TryParse(
                        lineData[1],
                        out SlideType type
                    ))
                        return (id, new SlideRecord(id, type, lineData[2]));
                }
                return (default(int), default(SlideRecord));
            }
            )
            .Where(x => x != (default(int), default(SlideRecord)))
            .ToDictionary(data => data.Item1, data => data.Item2);
    }

    /// <param name="lines">все строки файла, которые нужно распарсить. Первая строка — заголовочная.</param>
    /// <param name="slides">Словарь информации о слайдах по идентификатору слайда. 
    /// Такой словарь можно получить методом ParseSlideRecords</param>
    /// <returns>Список информации о посещениях</returns>
    /// <exception cref="FormatException">Если среди строк есть некорректные</exception>
    public static IEnumerable<VisitRecord> ParseVisitRecords(
        IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
    {
        return lines.Skip(1)
            .Select(line => Regex.Split(line, ";"))
            .Select(lineData =>
            {
                var errorMessage = "Wrong line [" + String.Join(";", lineData) + "]";
                if (lineData.Length != 4)
                    throw new FormatException(errorMessage);
                bool userParse = int.TryParse(lineData[0], out var userId);
                bool slideParse = int.TryParse(lineData[1], out var slideId);
                bool slideContains = slides.ContainsKey(slideId);
                bool dataParse = DateTime.TryParse(
                    lineData[2] + " " + lineData[3],
                    out DateTime date
                );
                if (!userParse || !slideParse || !dataParse || !slideContains)
                    throw new FormatException(errorMessage);
                return new VisitRecord(userId, slideId, date, slides[slideId].SlideType);
            })
            .ToArray();
    }
}