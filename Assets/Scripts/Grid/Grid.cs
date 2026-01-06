using UnityEngine;

public class Grid
{
    public int width;
    public int height;
    public float cellSize;
    public Cell[,] cells;

    public Grid(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        cells = new Cell[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cells[x, y] = new Cell(x, y);
            }
        }
    }

    public bool Inside(int x, int y)
    {
        return x >= 0 && y >= 0 && x < width && y < height;
    }

    public Cell Get(int x, int y)
    {
        return cells[x, y];
    }

    public Vector3 GridToWorld(int x, int y)
    {
        return new Vector3((x + 0.5f) * cellSize, 0, (y + 0.5f) * cellSize);
    }

    public Vector3 GridToWorld(Unit unit)
    {
        return GridToWorld(unit.x, unit.y);
    }

    public Vector2Int WorldToGrid(Vector3 world)
    {
        int x = Mathf.RoundToInt(world.x / cellSize);
        int y = Mathf.RoundToInt(world.z / cellSize);
        return new Vector2Int(x, y);
    }
}
