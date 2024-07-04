using System.Collections.Generic;
using System.Linq;
using Greedy.Architecture;

namespace Greedy;
public class DijkstraData
{
    public Point? Previous { get; set; }
    public int Price { get; set; }
}
public class DijkstraPathFinder
{
    public IEnumerable<PathWithCost> GetPathsByDijkstra(State state, Point start,
        IEnumerable<Point> targets)
    {
        HashSet<Point> chests = new HashSet<Point>(targets);
        HashSet<Point> candidatesToOpen = new HashSet<Point>();
        HashSet<Point> visitedNodes = new HashSet<Point>();
        var track = new Dictionary<Point, DijkstraData>();
        track[start] = new DijkstraData { Price = 0, Previous = null };
        candidatesToOpen.Add(start);
        while (true)
        {
            Point? toOpen = null;
            var bestPrice = int.MaxValue;
            foreach (var point in candidatesToOpen)
            {
                if (track[point].Price < bestPrice)
                {
                    bestPrice = track[point].Price;
                    toOpen = point;
                }
            }
            if (toOpen == null) yield break;
            if (chests.Contains(toOpen.Value)) yield return GetPath(track, toOpen.Value);
            NextPoints(state, candidatesToOpen, visitedNodes, track,
                toOpen, FindNextPoints(toOpen.Value, state));
            candidatesToOpen.Remove(toOpen.Value);
            visitedNodes.Add(toOpen.Value);
        }
    }

    private static void NextPoints(State state, HashSet<Point> candidatesToOpen,
        HashSet<Point> visitedNodes, Dictionary<Point, DijkstraData> track, 
        Point? toOpen, IEnumerable<Point> incidentPoints)
    {
        foreach (var incidentNode in incidentPoints)
        {
            var currentPrice = track[toOpen.Value].Price + state.CellCost[incidentNode.X, incidentNode.Y];
            if (!track.ContainsKey(incidentNode) || track[incidentNode].Price > currentPrice)
            {
                track[incidentNode] = new DijkstraData { Previous = toOpen, Price = currentPrice };
            }
            if (!visitedNodes.Contains(incidentNode))
                candidatesToOpen.Add(incidentNode);
        }
    }

    private static IEnumerable<Point> FindNextPoints(Point node, State state)
    {
        return new Point[]
        {
            new Point(node.X, node.Y+1),
            new Point(node.X, node.Y-1),
            new Point(node.X+1, node.Y),
            new Point(node.X-1, node.Y)
        }.Where(point => state.InsideMap(point) && !state.IsWallAt(point));
    }

    private static PathWithCost GetPath(Dictionary<Point, DijkstraData> track, Point end)
    {
        var res = new List<Point>();
        Point? currentPoint = end;
        while (currentPoint != null)
        {
            res.Add(currentPoint.Value);
            currentPoint = track[currentPoint.Value].Previous;
        }
        res.Reverse();
        return new PathWithCost(track[end].Price, res.ToArray());
    }
}