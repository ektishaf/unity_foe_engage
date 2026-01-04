using UnityEngine;

public static class GridUtility
{
    public static Vector3 GridToWorld(int x, int y, float cellSize)
    {
        return new Vector3((x + 0.5f) * cellSize, 0, (y + 0.5f) * cellSize);
    }

    public static Vector3 GridToWorld(Unit unit, float cellSize)
    {
        return new Vector3((unit.x + 0.5f) * cellSize, 0, (unit.y + 0.5f) * cellSize);
    }

    public static Vector2Int WorldToGrid(Vector3 world, float cellSize)
    {
        int x = Mathf.RoundToInt(world.x / cellSize);
        int y = Mathf.RoundToInt(world.z / cellSize);
        return new Vector2Int(x, y);
    }
}
