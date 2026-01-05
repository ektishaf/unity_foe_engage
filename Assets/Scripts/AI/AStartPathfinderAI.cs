using System.Collections.Generic;
using UnityEngine;

public static class AStarPathfinderAI
{
    public static List<Vector2Int> FindPath(
        Grid grid,
        Vector2Int start,
        Vector2Int goal)
    {
        var open = new List<PathNode>();
        var closed = new HashSet<Vector2Int>();

        PathNode startNode = new PathNode(start)
        {
            gCost = 0,
            hCost = Heuristic(start, goal)
        };

        open.Add(startNode);

        while (open.Count > 0)
        {
            open.Sort((a, b) => a.FCost.CompareTo(b.FCost));
            PathNode current = open[0];
            open.RemoveAt(0);

            if (current.pos == goal)
                return Reconstruct(current);

            closed.Add(current.pos);

            foreach (var d in GridUtility.Directions)
            {
                Vector2Int n = current.pos + d;
                if (!grid.Inside(n.x, n.y)) continue;
                if (!grid.Get(n.x, n.y).walkable) continue;
                if (closed.Contains(n)) continue;

                int g = current.gCost + grid.Get(n.x, n.y).moveCost;

                PathNode node = open.Find(x => x.pos == n);
                if (node == null)
                {
                    node = new PathNode(n)
                    {
                        gCost = g,
                        hCost = Heuristic(n, goal),
                        parent = current
                    };
                    open.Add(node);
                }
                else if (g < node.gCost)
                {
                    node.gCost = g;
                    node.parent = current;
                }
            }
        }

        return null;
    }

    static int Heuristic(Vector2Int a, Vector2Int b)
        => Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);

    static List<Vector2Int> Reconstruct(PathNode node)
    {
        var path = new List<Vector2Int>();
        while (node.parent != null)
        {
            path.Add(node.pos);
            node = node.parent;
        }
        path.Reverse();
        return path;
    }
}
