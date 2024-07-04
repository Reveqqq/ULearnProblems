using System;

namespace Rectangles
{
    public static class RectanglesTask
    {

        public static bool AreIntersected(Rectangle r1, Rectangle r2)
        {
            int Left_Overlap = Math.Max(r1.Left, r2.Left);
            int Right_Overlap = Math.Min(r1.Right, r2.Right);
            int Top_Overlap = Math.Max(r1.Top, r2.Top);
            int Bottom_Overlap = Math.Min(r1.Bottom, r2.Bottom);
            if ((Left_Overlap > Right_Overlap || Top_Overlap > Bottom_Overlap))
                return false;
            else
                return true;
        }

        public static int IntersectionSquare(Rectangle r1, Rectangle r2)
        {
            int Left_Overlap = Math.Max(r1.Left, r2.Left);
            int Right_Overlap = Math.Min(r1.Right, r2.Right);
            int Top_Overlap = Math.Max(r1.Top, r2.Top);
            int Bottom_Overlap = Math.Min(r1.Bottom, r2.Bottom);
            if (AreIntersected(r1, r2))
                return ((Right_Overlap - Left_Overlap) * (Bottom_Overlap - Top_Overlap));
            else return 0;
        }

        public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
        {
            if (r2.Height * r2.Width == IntersectionSquare(r1, r2) && r2.Left >= r1.Left)
                return 1;
            else if (r1.Height * r1.Width == IntersectionSquare(r1, r2) || r2.Top - r2.Height == r1.Top && r2.Height == 0
                && r2.Left >= r1.Left)
                return 0;
            else
                return -1;

        }
    }
}