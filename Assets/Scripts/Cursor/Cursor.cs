using UnityEngine;

public class Cursor
{
    Grid grid;
    public int x, y;

    public Grid GridRef {  get { return grid; } }

    public Cursor(int startX, int startY, Grid grid)
    {
        x = startX;
        y = startY;
        this.grid = grid;
    }

    public void Move(int dx, int dy)
    {
        int newX = Mathf.Clamp(x + dx, 0, grid.width - 1);
        int newY = Mathf.Clamp(y + dy, 0, grid.height - 1);
        x = newX;
        y = newY;
    }

    public void SetPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Vector2Int Position => new Vector2Int(x, y);
}
