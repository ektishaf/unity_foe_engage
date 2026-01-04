using System.Collections.Generic;
using UnityEngine;

public static class MovementSystem
{
    public static HashSet<Vector2Int> GetMoveRange(GridData grid, Unit unit)
    {
        var visited = new HashSet<Vector2Int>();
        var queue = new Queue<(Vector2Int pos, int cost)>();
        queue.Enqueue((unit.GridPos, 0));

        while (queue.Count > 0)
        {
            var (pos, cost) = queue.Dequeue();
            if (visited.Contains(pos)) continue;
            visited.Add(pos);

            if (cost >= unit.movementPoints) continue;

            Vector2Int[] dirs = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
            foreach (var d in dirs)
            {
                Vector2Int n = pos + d;
                if (!grid.Inside(n.x, n.y)) continue;
                if (!grid.Get(n.x, n.y).walkable) continue;
                if (grid.Get(n.x, n.y).occupied) continue;

                queue.Enqueue((n, cost + grid.Get(n.x, n.y).moveCost));
            }
        }

        visited.Remove(unit.GridPos); // optional: remove current position
        return visited;
    }

    public static HashSet<Vector2Int> GetAttackRange(GridData grid, Unit unit)
    {
        var attackTiles = new HashSet<Vector2Int>();
        for (int dx = -unit.attackRange; dx <= unit.attackRange; dx++)
        {
            for (int dy = -unit.attackRange; dy <= unit.attackRange; dy++)
            {
                int nx = unit.x + dx;
                int ny = unit.y + dy;
                if (!grid.Inside(nx, ny)) continue;

                // Manhattan distance for attack range
                if (Mathf.Abs(dx) + Mathf.Abs(dy) <= unit.attackRange)
                    attackTiles.Add(new Vector2Int(nx, ny));
            }
        }
        return attackTiles;
    }
}
