using System;
using System.Collections.Generic;

// Каждый документ — это список токенов. То есть List<string>.
// Вместо этого будем использовать псевдоним DocumentTokens.
// Это поможет избежать сложных конструкций:
// вместо List<List<string>> будет List<DocumentTokens>
using DocumentTokens = System.Collections.Generic.List<string>;

namespace Antiplagiarism;

public class LevenshteinCalculator
{
    public List<ComparisonResult> CompareDocumentsPairwise(List<DocumentTokens> documents)
    {
        var comparisons = new List<ComparisonResult>();
        for (int i = 0; i < documents.Count - 1; i++)
            for (int j = i + 1; j < documents.Count; j++)
            {
                    var dist = LevenshteinDistance(documents[i],documents[j]);
                    comparisons.Add(new(
                    documents[i],
                    documents[j],
                    dist));
            }
        return comparisons;
    }

    public static double LevenshteinDistance(DocumentTokens first, DocumentTokens second)
    {
        var opt = new double[first.Count + 1, second.Count + 1];
        for (var i = 0; i <= first.Count; ++i)
            opt[i, 0] = i;
        for (var i = 0; i <= second.Count; ++i)
            opt[0, i] = i;
        for (var i = 1; i <= first.Count; ++i)
            for (var j = 1; j <= second.Count; ++j)
            {
                if (first[i - 1].Equals(second[j - 1]))
                    opt[i, j] = opt[i - 1, j - 1];
                else
                {
                    opt[i, j] = MinOpt(1 + opt[i - 1, j],
                        TokenDistanceCalculator.GetTokenDistance(first[i - 1], second[j - 1]) + opt[i - 1, j - 1],
                        1 + opt[i, j - 1]);
                }
            }
        return opt[first.Count, second.Count];
    }

    private static double MinOpt(double x, double y, double z)
    {
        return x < y ? (x < z ? x : z) : (y < z ? y : z);
    }
}