using System.Collections;
using System.Collections.Generic;

namespace Rivals;

public class RivalsTask
{
    public static IEnumerable<OwnedLocation> AssignOwners(Map map)
    {
        var track = new Dictionary<Point, OwnedLocation>();
        var queue = new Queue<OwnedLocation>();
        for (int i = 0; i < map.Players.Length; i++)
        {
            var tmp = new OwnedLocation(i, map.Players[i], 0);
            track[map.Players[i]] = tmp;
            queue.Enqueue(tmp);
        }
        while (queue.Count != 0)
        {
            var ownedLoc = queue.Dequeue();
            if (ownedLoc.Location.X < 0 || ownedLoc.Location.Y < 0 || !map.InBounds(ownedLoc.Location)
                || map.Maze[ownedLoc.Location.X, ownedLoc.Location.Y] != MapCell.Empty) continue;
            yield return ownedLoc;
            NextPoint(track, queue, ownedLoc);
        }
    }

    private static void NextPoint(Dictionary<Point, OwnedLocation> track, 
        Queue<OwnedLocation> queue, OwnedLocation ownedLoc)
    {
        for (var dy = -1; dy <= 1; dy++)
            for (var dx = -1; dx <= 1; dx++)
            {
                var nextPoint = new Point { X = ownedLoc.Location.X + dx, Y = ownedLoc.Location.Y + dy };
                if ((dx != 0 && dy != 0) || track.ContainsKey(nextPoint)) continue;
                else
                {
                    var tmp = new OwnedLocation(ownedLoc.Owner, nextPoint, ownedLoc.Distance + 1);
                    track[nextPoint] = tmp;
                    queue.Enqueue(tmp);
                }
            }
    }
}