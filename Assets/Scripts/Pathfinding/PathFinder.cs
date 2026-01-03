using System.Collections.Generic;
using UnityEngine;

public static class PathFinder
{
    public static List<Vector2Int> FindPath(
        Unit unit,
        Vector2Int target,
        GridManager grid,
        Unit[,] unitMap)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Vector2Int start = new Vector2Int(unit.x, unit.y);

        if (start == target) return path;

        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();

        queue.Enqueue(start);
        cameFrom[start] = start;

        Vector2Int[] dirs =
        {
            Vector2Int.up, Vector2Int.down,
            Vector2Int.left, Vector2Int.right
        };

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();
            if (current == target) break;

            foreach (var d in dirs)
            {
                Vector2Int next = current + d;
                if (!grid.Inside(next.x, next.y)) continue;
                if (unitMap[next.x, next.y] != null && next != target) continue;
                if (cameFrom.ContainsKey(next)) continue;

                cameFrom[next] = current;
                queue.Enqueue(next);
            }
        }

        if (!cameFrom.ContainsKey(target)) return path;

        Vector2Int c = target;
        while (c != start)
        {
            path.Add(c);
            c = cameFrom[c];
        }

        path.Reverse();
        return path;
    }
}
