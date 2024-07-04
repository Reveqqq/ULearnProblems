using System.Collections.Generic;

namespace Dungeon;

public class BfsTask
{
    public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
    {
        var track = new Dictionary<Point, SinglyLinkedList<Point>>();
        var hashChests = new HashSet<Point>(chests);
        var queue = new Queue<SinglyLinkedList<Point>>();
        queue.Enqueue(new SinglyLinkedList<Point>(start));
        while (queue.Count != 0)
        {
            var listPoint = queue.Dequeue();
            if (listPoint.Value.X < 0 || listPoint.Value.Y < 0 || !map.InBounds(listPoint.Value)
                || map.Dungeon[listPoint.Value.X, listPoint.Value.Y] != MapCell.Empty) continue;
            for (var dy = -1; dy <= 1; dy++)
                for (var dx = -1; dx <= 1; dx++)
                {
                    var nextPoint = new Point { X = listPoint.Value.X + dx, Y = listPoint.Value.Y + dy };
                    if ((dx != 0 && dy != 0) || track.ContainsKey(nextPoint)) continue;
                    else
                    {
                        var tmpList = new SinglyLinkedList<Point>(nextPoint, listPoint);
                        track[nextPoint] = tmpList;
                        queue.Enqueue(tmpList);
                        if (hashChests.Contains(nextPoint))
                            yield return track[nextPoint];
                    }
                }
        }
    }
}