public class Grid
{
    public int width;
    public int height;
    public float cellSize;
    public GridCell[,] cells;

    public Grid(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        cells = new GridCell[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cells[x, y] = new GridCell(x, y);
            }
        }
    }

    public bool Inside(int x, int y)
    {
        return x >= 0 && y >= 0 && x < width && y < height;
    }

    public GridCell Get(int x, int y)
    {
        return cells[x, y];
    }
}
