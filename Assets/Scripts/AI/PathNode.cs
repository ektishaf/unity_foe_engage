using UnityEngine;

public class PathNode
{
    public Vector2Int pos;
    public int gCost;
    public int hCost;
    public PathNode parent;

    public int FCost => gCost + hCost;

    public PathNode(Vector2Int p) => pos = p;
}
