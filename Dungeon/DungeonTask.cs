using System;
using System.Collections.Generic;
using System.Linq;

namespace Dungeon;

public class DungeonTask
{
    public static MoveDirection[] FindShortestPath(Map map)
    {
        var moves = new List<MoveDirection>();
        var pathFromStart = BfsTask.FindPaths(map, map.InitialPosition, map.Chests);
        var pathFromExit = BfsTask.FindPaths(map, map.Exit, map.Chests);
        var pathFromStartToExit = BfsTask.FindPaths(map, map.Exit, new Point[] { map.InitialPosition })
            .FirstOrDefault();
        var pathes = pathFromStart.Join(pathFromExit, e => e.Value, o => o.Value, (e, o) => Tuple.Create(e, o));
        var tuplePath = pathes
            .OrderBy(x => x.Item1.Length + x.Item2.Length)
            .FirstOrDefault();
        
        if (tuplePath != null)
        {
            var path = tuplePath.Item1.Reverse().Concat(tuplePath.Item2.Skip(1)).ToArray();
            CreateMoves(moves, path);
        }
        else if (pathFromStartToExit != null)
        {
            var path = pathFromStartToExit.ToArray();
            CreateMoves(moves, path);
        }

        return moves.ToArray();
    }

    private static void CreateMoves(List<MoveDirection> moves, Point[] path)
    {
        for (int i = 1; i < path.Length; i++)
            moves.Add(Walker.ConvertOffsetToDirection(path[i] - path[i - 1]));
    }
}