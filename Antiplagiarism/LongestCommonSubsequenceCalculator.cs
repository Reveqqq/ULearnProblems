﻿using System;
using System.Collections.Generic;

namespace Antiplagiarism;

public static class LongestCommonSubsequenceCalculator
{
    public static List<string> Calculate(List<string> first, List<string> second)
    {
        var opt = CreateOptimizationTable(first, second);
        return RestoreAnswer(opt, first, second);
    }

    private static int[,] CreateOptimizationTable(List<string> first, List<string> second)
    {
        var opt = new int[first.Count + 1, second.Count + 1];
        for (var i = 0; i <= first.Count; ++i)
            opt[i, 0] = 0;
        for (var i = 0; i <= second.Count; ++i)
            opt[0, i] = 0;
        for (var i = 1; i <= first.Count; ++i)
            for (var j = 1; j <= second.Count; ++j)
            {
                if (first[i - 1].Equals(second[j - 1]))
                    opt[i, j] = opt[i - 1, j - 1] + 1;
                else
                {
                    opt[i, j] = Math.Max(opt[i - 1, j], opt[i, j - 1]);
                }
            }
        return opt;
    }

    private static List<string> RestoreAnswer(int[,] opt, List<string> first, List<string> second)
    {
        int i = first.Count;
        int j = second.Count;
        var ans = new List<string>();
        while (opt[i, j] != 0)
        {
            if (opt[i, j] == opt[i - 1, j])
                i -= 1;
            else if (opt[i, j] == opt[i, j - 1])
                j -= 1;
            else
            {
                ans.Add(first[i - 1]);
                i -= 1;
                j -= 1;
            }
        }
        ans.Reverse();
        return ans;
    }
}