using System.Collections.Generic;
using UnityEngine;

public static class PathFinder
{
    public static List<Vector2Int> FindPathAStar(Unit unit, Vector2Int target, GridData grid, Unit[,] unitMap)
    {
        Vector2Int start = unit.GridPos;
        List<Vector2Int> path = new List<Vector2Int>();

        if (start == target) return path;

        // OPEN SET (tiles to evaluate)
        List<Vector2Int> openSet = new List<Vector2Int>();
        openSet.Add(start);

        // CLOSED SET (already evaluated)
        HashSet<Vector2Int> closedSet = new HashSet<Vector2Int>();

        // Cost from start
        Dictionary<Vector2Int, int> gCost = new Dictionary<Vector2Int, int>();
        gCost[start] = 0;

        // For path reconstruction
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();

        Vector2Int[] dirs =
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };

        while (openSet.Count > 0)
        {
            // Pick tile with lowest f = g + h
            Vector2Int current = openSet[0];
            int bestF = GetFCost(current, target, gCost);

            for (int i = 1; i < openSet.Count; i++)
            {
                int f = GetFCost(openSet[i], target, gCost);
                if (f < bestF)
                {
                    bestF = f;
                    current = openSet[i];
                }
            }

            if (current == target)
                return ReconstructPath(cameFrom, current);

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (var d in dirs)
            {
                Vector2Int next = current + d;

                if (!grid.Inside(next.x, next.y))
                    continue;

                if (!grid.Get(next.x, next.y).walkable)
                    continue;

                if (unitMap[next.x, next.y] != null && next != target)
                    continue;

                if (closedSet.Contains(next))
                    continue;

                int moveCost = grid.Get(next.x, next.y).moveCost;
                int tentativeG = gCost[current] + moveCost;

                if (!gCost.ContainsKey(next) || tentativeG < gCost[next])
                {
                    cameFrom[next] = current;
                    gCost[next] = tentativeG;

                    if (!openSet.Contains(next))
                        openSet.Add(next);
                }
            }
        }

        // No path found
        return path;
    }

    static int GetFCost(Vector2Int pos, Vector2Int target, Dictionary<Vector2Int, int> gCost)
    {
        return gCost[pos] + Heuristic(pos, target);
    }

    static int Heuristic(Vector2Int a, Vector2Int b)
    {
        // Manhattan distance (perfect for grid tactics games)
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    static List<Vector2Int> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int current)
    {
        List<Vector2Int> path = new List<Vector2Int>();

        while (cameFrom.ContainsKey(current))
        {
            path.Add(current);
            current = cameFrom[current];
        }

        path.Reverse();
        return path;
    }
}
