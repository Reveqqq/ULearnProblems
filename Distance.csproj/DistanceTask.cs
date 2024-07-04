using System;

namespace DistanceTask
{
    public static class DistanceTask
    {

        public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
        {
            if ((x == ax && y == ay) || (x == bx && y == by))
                return 0;
            if (ax == bx && ay == by)
                return Math.Sqrt(Math.Pow(ax - x, 2) + Math.Pow(ay - y, 2));
            double[] AB = { bx - ax, by - ay };
            double[] BE = { x - bx, y - by };
            double[] AE = { x - ax, y - ay };
            double AB_BE = AB[0] * BE[0] + AB[1] * BE[1];
            double AB_AE = AB[0] * AE[0] + AB[1] * AE[1];
            double reqAns = 0.0;
            if (AB_BE > 0)
            {
                double Y = y - by;
                double X = x - bx;
                reqAns = Math.Sqrt(X * X + Y * Y);
            }
            else if (AB_AE < 0)
            {
                double Y = y - ay;
                double X = x - ax;
                reqAns = Math.Sqrt(X * X + Y * Y);
            }
            else
            {
                double x1 = AB[0];
                double y1 = AB[1];
                double x2 = AE[0];
                double y2 = AE[1];
                double mod = Math.Sqrt(x1 * x1 + y1 * y1);
                reqAns = Math.Abs(x1 * y2 - y1 * x2) / mod;
            }
            return reqAns;
        }
    }
}