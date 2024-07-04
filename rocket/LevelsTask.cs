using HarfBuzzSharp;
using System;
using System.Collections.Generic;

namespace func_rocket;

public class LevelsTask
{
    private static Gravity? gravity1;
    private static Gravity? gravity2;
    private static Vector Target = new(600, 200);
    private static readonly Rocket RocketStart = new(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI);
    static readonly Physics standardPhysics = new();

    public static Level CreateLevel(string name, Rocket rocketStart, Vector target, Gravity gravity)
    {
        return new Level(name, rocketStart, target, gravity, standardPhysics);
    }

    public static IEnumerable<Level> CreateLevels()
    {
        yield return CreateLevel("Zero", RocketStart, Target, (size, v) => Vector.Zero);
        yield return CreateLevel("Heavy", RocketStart, Target, (size, v) => new Vector(0, 0.9));
        yield return CreateLevel("Up", RocketStart, new Vector(700, 500),
            (size, v) => new Vector(0, -300 / (size.Y - v.Y + 300.0)));
        yield return CreateLevel("WhiteHole", RocketStart, Target, gravity1 = (size, v) =>
        {
            var d = (Target - v).Length;
            return (Target - v).Normalize() * (-140 * d) / (d * d + 1);
        });
        yield return CreateLevel("BlackHole", RocketStart, Target, gravity2 = (size, v) =>
        {
            var anomaly = (Target + RocketStart.Location) / 2;
            var d = (anomaly - v).Length;
            return (anomaly - v).Normalize() * (300 * d) / (d * d + 1);
        });
        yield return CreateLevel("BlackAndWhite", RocketStart, Target,
            (size, v) => (gravity1(size, v) + gravity2(size, v)) / 2);
    }
}