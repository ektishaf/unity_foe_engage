using System.Collections.Generic;
using UnityEngine;

public static class AttackRange
{
    public static HashSet<Vector2Int> Calculate(Unit u, GridManager grid)
    {
        HashSet<Vector2Int> result = new HashSet<Vector2Int>();

        for (int dx = -u.attackRange; dx <= u.attackRange; dx++)
            for (int dy = -u.attackRange; dy <= u.attackRange; dy++)
            {
                int nx = u.x + dx;
                int ny = u.y + dy;
                if (grid.Inside(nx, ny))
                    result.Add(new Vector2Int(nx, ny));
            }

        return result;
    }
}
