using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryTasks
{
    public class Vector
    {
        public double X;
        public double Y;
        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public Vector Add(Vector vector)
        {
            return Geometry.Add(this, vector);
        }

        public bool Belongs(Segment segment)
        {
            return Geometry.IsVectorInSegment(this, segment);
        }
    }

    public class Geometry
    {
        public static double GetLength(Vector vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static Vector Add(Vector vector1, Vector vector2)
        {
            var addVector = new Vector
            {
                X = vector1.X + vector2.X,
                Y = vector1.Y + vector2.Y
            };
            return addVector;
        }

        public static double GetLength(Segment segment)
        {
            return Math.Sqrt((segment.End.X - segment.Begin.X) * (segment.End.X - segment.Begin.X)
                + (segment.End.Y - segment.Begin.Y) * (segment.End.Y - segment.Begin.Y));
        }

        public static bool IsVectorInSegment(Vector vector, Segment segment)
        {
            bool isVectorInSegment1 = false;
            bool isVectorInSegment2 = false;
            bool isVectorInSegment3 = false;
            if ((vector.X - segment.Begin.X) * (segment.End.Y - segment.Begin.Y) -
                    (segment.End.X - segment.Begin.X) * (vector.Y - segment.Begin.Y) == 0)
                isVectorInSegment1 = true;
            if ((vector.X - segment.Begin.X) * (segment.End.X - vector.X) >= 0
                && (vector.X - segment.Begin.X) * (segment.End.X - vector.X)
                <= (segment.Begin.X - segment.End.X) * (segment.Begin.X - segment.End.X))
                isVectorInSegment2 = true;
            if ((vector.Y - segment.Begin.Y) * (segment.End.Y - vector.Y) >= 0
                && (vector.Y - segment.Begin.Y) * (segment.End.Y - vector.Y)
                <= (segment.Begin.Y - segment.End.Y) * (segment.Begin.Y - segment.End.Y))
                isVectorInSegment3 = true;
            return isVectorInSegment1 && isVectorInSegment2 && isVectorInSegment3;
        }
    }

    public class Segment
    {
        public Vector Begin;
        public Vector End;

        public double GetLength()
        {
            return Geometry.GetLength(this);
        }

        public bool Contains(Vector vector)
        {
            return Geometry.IsVectorInSegment(vector, this);
        }
    }
}
