using UnityEngine;

public enum FogState
{
    Hidden,
    Explored,
    Visible
}

public class GridCell
{
    public Vector2Int position;
    public bool walkable = true;
    public int moveCost = 1;
    public int height = 0;
    public bool occupied = false;
    public FogState fogState = FogState.Hidden;

    public GridCell(int x, int y)
    {
        position = new Vector2Int(x, y);
    }
}