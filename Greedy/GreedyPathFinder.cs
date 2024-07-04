using System.Collections.Generic;
using System.Linq;
using Greedy.Architecture;
using JetBrains.Annotations;

namespace Greedy;

public class GreedyPathFinder : IPathFinder
{
    private DijkstraPathFinder pathFinder = new DijkstraPathFinder();

    public List<Point> FindPathToCompleteGoal(State state)
    {
        var chests = new HashSet<Point>(state.Chests);
        var ans = new List<Point>();
        var goal = state.Goal;
        if (goal <= state.Chests.Count)
        {
            while (goal != 0)
            {
                var path = pathFinder
                    .GetPathsByDijkstra(state, state.Position, chests)
                    .FirstOrDefault();
                if (path == null || path.Cost > state.Energy) break;
                ans = ans.Concat(path.Path.Skip(1)).ToList();
                state.Energy -= path.Cost;
                state.Position = path.End;
                chests.Remove(path.End);
                goal--;
            }
        }
        return ans;
    }
}