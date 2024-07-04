using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryTasks;

namespace GeometryPainting
{
    public static class SegmentExtensions
    {
        public static Dictionary<Segment, Color> SegmentColor = new Dictionary<Segment, Color>();
        public static void SetColor(this Segment segment, Color color)
        {
            if (!SegmentExtensions.SegmentColor.ContainsKey(segment))
                SegmentExtensions.SegmentColor.Add(segment, color);
            else
                SegmentExtensions.SegmentColor[segment] = color;
        }

        public static Color GetColor(this Segment segment)
        {
            if (SegmentExtensions.SegmentColor.ContainsKey(segment))
                if (!SegmentExtensions.SegmentColor[segment].IsEmpty)
                    return SegmentExtensions.SegmentColor[segment];
            return Color.Black;
        }
    }
}