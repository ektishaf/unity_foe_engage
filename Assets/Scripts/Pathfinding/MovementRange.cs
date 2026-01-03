using System.Collections.Generic;
using UnityEngine;

public static class MovementRange
{
    public static HashSet<Vector2Int> Calculate(
        Unit unit,
        GridManager grid,
        Unit[,] unitMap)
    {
        HashSet<Vector2Int> result = new HashSet<Vector2Int>();
        Queue<(Vector2Int pos, int cost)> q = new Queue<(Vector2Int, int)>();

        Vector2Int start = new Vector2Int(unit.x, unit.y);
        q.Enqueue((start, 0));
        result.Add(start);

        Vector2Int[] dirs =
        {
            Vector2Int.up, Vector2Int.down,
            Vector2Int.left, Vector2Int.right
        };

        while (q.Count > 0)
        {
            var node = q.Dequeue();

            foreach (var d in dirs)
            {
                Vector2Int next = node.pos + d;
                if (!grid.Inside(next.x, next.y)) continue;
                if (unitMap[next.x, next.y] != null) continue;

                int cost = node.cost + 1;
                if (cost > unit.movementPoints) continue;

                if (result.Add(next))
                    q.Enqueue((next, cost));
            }
        }

        return result;
    }
}
