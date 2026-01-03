using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public float cellSize = 1f;

    public GridCell[,] Cells { get; private set; }

    void Awake()
    {
        Cells = new GridCell[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cells[x, y] = new GridCell();
            }
        }
    }

    public bool Inside(int x, int y)
    {
        return x >= 0 && y >= 0 && x < width && y < height;
    }

    public Vector3 GridToWorld(int x, int y)
    {
        return transform.position + new Vector3((x + 0.5f) * cellSize, 0, (y + 0.5f) * cellSize);
    }
}
