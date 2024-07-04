using System;

namespace GaussAlgorithm;

public class Solver
{
    private const double Eps = 1e-5;

    private double DoZero(double d)
    {
        return Math.Abs(d) < Eps ? 0 : d;
    }

    public double[] Solve(double[][] matrix, double[] freeMembers)
    {
        var rowCount = matrix.GetLength(0);
        var columnCount = matrix[0].Length;
        var ans = new double[columnCount];
        bool[] markedRows = new bool[rowCount];
        double[][] extendedMatrix = new double[rowCount][];

        MakeExtendedMatrix(matrix, freeMembers, rowCount, columnCount, extendedMatrix);
        DoGaussMethod(rowCount, columnCount, markedRows, extendedMatrix);
        if (IsZeroRow(extendedMatrix, rowCount, columnCount))
            throw new NoSolutionException("Was Zero Row");

        markedRows = new bool[rowCount];
        MakeSolution(rowCount, columnCount, ans, markedRows, extendedMatrix);
        return ans;
    }

    private void MakeSolution(int rowCount, int columnCount, double[] ans, bool[] markedRows, double[][] extendedMatrix)
    {
        for (int i = 0; i < columnCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
                if (j < rowCount && extendedMatrix[j][i] != 0 && !markedRows[j])
                {
                    ans[i] = DoZero(extendedMatrix[j][columnCount] / extendedMatrix[j][i]);
                    markedRows[j] = true;
                    break;
                }
        }
    }

    private void DoGaussMethod(int rowCount, int columnCount, bool[] markedRows, double[][] extendedMatrix)
    {
        int k = 0;
        while (k < columnCount)
        {
            int y = 0;
            while (y < rowCount && (markedRows[y] || extendedMatrix[y][k] == 0))
                y++;

            if (y != rowCount)
            {
                for (int i = 0; i < rowCount; i++)
                {
                    if (i == y)
                        continue;
                    var ratio = extendedMatrix[i][k] / extendedMatrix[y][k];
                    for (int j = 0; j < columnCount + 1; j++) // + 1 для изменения свободного значения
                        extendedMatrix[i][j] = DoZero(extendedMatrix[i][j] - extendedMatrix[y][j] * ratio);
                }
                markedRows[y] = true;
            }
            k++;
        }
    }

    private void MakeExtendedMatrix(double[][] matrix, double[] freeMembers,
        int rowCount, int columnCount, double[][] extendedMatrix)
    {
        for (int i = 0; i < rowCount; i++)
        {
            extendedMatrix[i] = new double[columnCount + 1]; // +1 для свободных значений
            for (int j = 0; j < columnCount; j++)
                extendedMatrix[i][j] = DoZero(matrix[i][j]);
            extendedMatrix[i][columnCount] = freeMembers[i];
        }
    }

    private bool IsZeroRow(double[][] extextendedMatrix, int rowCount, int columnCount)
    {
        bool res = false;
        for (int i = 0; i < rowCount && !res; i++)
        {
            double sum = 0;
            for (int j = 0; j < columnCount && !res; j++)
                sum += DoZero(extextendedMatrix[i][j]);

            if (sum == 0 && extextendedMatrix[i][columnCount] != 0)
                res = true;
        }
        return res;
    }
}