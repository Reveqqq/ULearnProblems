using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace RefactorMe
{
    class Drawer
    {
        static float x, y;
        static Graphics graphic;

        public static void Convert(Graphics newGraphics)
        {
            graphic = newGraphics;
            graphic.SmoothingMode = SmoothingMode.None;
            graphic.Clear(Color.Black);
        }

        public static void SetPosition(float x0, float y0)
        {
            x = x0;
            y = y0;
        }

        public static void MakeIt(Pen pencil, double length, double angel)
        {
            //Делает шаг длиной length в направлении angel и рисует пройденную траекторию
            var x1 = (float)(x + length * Math.Cos(angel));
            var y1 = (float)(y + length * Math.Sin(angel));
            graphic.DrawLine(pencil, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void Change(double length, double angel)
        {
            x = (float)(x + length * Math.Cos(angel));
            y = (float)(y + length * Math.Sin(angel));
        }
    }

    public class ImpossibleSquare
    {
        const float Ang1 = 0.375f;
        const float Ang2 = 0.04f;
        public static void Draw(int width, int height, double turnAngel, Graphics graphic)
        {
            // ugolPovorota пока не используется, но будет использоваться в будущем
            Drawer.Convert(graphic);
            var size = Math.Min(width, height);
            var diagonal_length = Math.Sqrt(2) * (size * Ang1 + size * Ang2) / 2;
            var x0 = (float)(diagonal_length * Math.Cos(Math.PI / 4 + Math.PI)) + width / 2f;
            var y0 = (float)(diagonal_length * Math.Sin(Math.PI / 4 + Math.PI)) + height / 2f;
            Drawer.SetPosition(x0, y0);
            FirstSide(size);
            SecondSide(size);
            ThirdSide(size);
            ForthSide(size);
        }

        private static void ForthSide(int size)
        {
            Drawer.MakeIt(Pens.Yellow, size * Ang1, Math.PI / 2);
            Drawer.MakeIt(Pens.Yellow, size * Ang2 * Math.Sqrt(2), Math.PI / 2 + Math.PI / 4);
            Drawer.MakeIt(Pens.Yellow, size * Ang1, Math.PI / 2 + Math.PI);
            Drawer.MakeIt(Pens.Yellow, size * Ang1 - size * Ang2, Math.PI / 2 + Math.PI / 2);
            Drawer.Change(size * Ang2, Math.PI / 2 - Math.PI);
            Drawer.Change(size * Ang2 * Math.Sqrt(2), Math.PI / 2 + 3 * Math.PI / 4);
        }

        private static void ThirdSide(int size)
        {
            Drawer.MakeIt(Pens.Yellow, size * Ang1, Math.PI);
            Drawer.MakeIt(Pens.Yellow, size * Ang2 * Math.Sqrt(2), Math.PI + Math.PI / 4);
            Drawer.MakeIt(Pens.Yellow, size * Ang1, Math.PI + Math.PI);
            Drawer.MakeIt(Pens.Yellow, size * Ang1 - size * Ang2, Math.PI + Math.PI / 2);
            Drawer.Change(size * Ang2, Math.PI - Math.PI);
            Drawer.Change(size * Ang2 * Math.Sqrt(2), Math.PI + 3 * Math.PI / 4);
        }

        private static void SecondSide(int size)
        {
            Drawer.MakeIt(Pens.Yellow, size * Ang1, -Math.PI / 2);
            Drawer.MakeIt(Pens.Yellow, size * Ang2 * Math.Sqrt(2), -Math.PI / 2 + Math.PI / 4);
            Drawer.MakeIt(Pens.Yellow, size * Ang1, -Math.PI / 2 + Math.PI);
            Drawer.MakeIt(Pens.Yellow, size * Ang1 - size * Ang2, -Math.PI / 2 + Math.PI / 2);
            Drawer.Change(size * Ang2, -Math.PI / 2 - Math.PI);
            Drawer.Change(size * Ang2 * Math.Sqrt(2), -Math.PI / 2 + 3 * Math.PI / 4);
        }

        private static void FirstSide(int size)
        {
            Drawer.MakeIt(Pens.Yellow, size * Ang1, 0);
            Drawer.MakeIt(Pens.Yellow, size * Ang2 * Math.Sqrt(2), Math.PI / 4);
            Drawer.MakeIt(Pens.Yellow, size * Ang1, Math.PI);
            Drawer.MakeIt(Pens.Yellow, size * Ang1 - size * Ang2, Math.PI / 2);
            Drawer.Change(size * Ang2, -Math.PI);
            Drawer.Change(size * Ang2 * Math.Sqrt(2), 3 * Math.PI / 4);
        }
    }
}