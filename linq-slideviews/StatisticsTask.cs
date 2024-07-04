using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews;

public class StatisticsTask
{
    public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
    {
        var T = visits.OrderBy(x => x.UserId)
            .ThenBy(x => x.DateTime)
            .Bigrams()
            .Where(x =>
        x.First.SlideType == slideType
        && x.First.SlideId != x.Second.SlideId
        && x.First.UserId == x.Second.UserId
        && (x.Second.DateTime - x.First.DateTime).TotalMinutes >= 1
        && (x.Second.DateTime - x.First.DateTime).TotalMinutes <= 120
        )
        .Select(x => (x.Second.DateTime - x.First.DateTime).TotalMinutes
        );
        return !T.Any() ? 0 : T.Median();
    }
}