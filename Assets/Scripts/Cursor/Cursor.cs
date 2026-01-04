using UnityEngine;

public class Cursor
{
    public int x, y;

    public Cursor(int startX, int startY)
    {
        x = startX;
        y = startY;
    }

    public void Move(int dx, int dy, Grid grid)
    {
        int newX = Mathf.Clamp(x + dx, 0, grid.width - 1);
        int newY = Mathf.Clamp(y + dy, 0, grid.height - 1);
        x = newX;
        y = newY;
    }

    public Vector2Int Position => new Vector2Int(x, y);
}
