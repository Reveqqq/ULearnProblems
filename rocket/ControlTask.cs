using System;

namespace func_rocket;

public class ControlTask
{
    public static double GetAngel(Vector a, Vector b)
    {
        return Math.Acos(
            (a.X * b.X + a.Y * b.Y) /
            (a.Length * b.Length)
            );
    }

    public static Turn ControlRocket(Rocket rocket, Vector target)
    {
        var targetVector = rocket.Location - target;
        var e = new Vector(1, 0);
        var moveVector = e.Rotate(rocket.Direction) + rocket.Velocity.Normalize();
        var alpha = GetAngel(targetVector, moveVector);
        if (alpha < 0.1)
            return Turn.None;
        var rightVector = moveVector.Rotate(alpha / 2);
        var leftVector = moveVector.Rotate(-alpha / 2);
        var rightAlpha = GetAngel(targetVector, rightVector);
        var leftAlpha = GetAngel(targetVector, leftVector);
        if (rightAlpha < leftAlpha)
            return Turn.Left;
        else
            return Turn.Right;
    }
}