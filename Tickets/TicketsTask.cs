using System;
using System.Numerics;

namespace Tickets;

class TicketsTask
{
    public static BigInteger Solve(int n, int totalSum)
    {
        if ((totalSum & 1) == 1)
            return 0;
        BigInteger[][] dp = new BigInteger[n+1][];
        for (int i = 0; i < 51; i++)
            dp[i] = new BigInteger[totalSum+1];

        totalSum >>= 1;

        dp[0][0] = BigInteger.One;
        for (int i = 1; i <= totalSum; ++i)
            dp[0][i] = BigInteger.Zero;

        for (int i = 1; i <= n; ++i)
            for (int j = 0; j <= totalSum; ++j)
            {
                dp[i][j] = BigInteger.Zero;
                for (int d = 0; d <= 9; ++d)
                    if (j >= d)
                        dp[i][j] += dp[i - 1][j - d];
            }
        return dp[n][totalSum] * dp[n][totalSum];
    }
}