using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PathPreview : MonoBehaviour
{
    Grid grid;
    LineRenderer line;

    public void Init(Grid grid)
    {
        this.grid = grid;
        line = GetComponent<LineRenderer>();
        line.positionCount = 0;
        line.useWorldSpace = true;
        line.widthMultiplier = 0.1f;
        //line.material = new Material(Shader.Find("Sprites/Default"));
        //line.startColor = Color.cyan;
        //line.endColor = Color.cyan;
    }

    public void DrawPath(List<Vector2Int> path)
    {
        if (path == null || path.Count == 0)
        {
            Clear();
            return;
        }

        line.positionCount = path.Count + 1;

        // Start at unit position
        line.SetPosition(0, GridUtility.GridToWorld(path[0].x, path[0].y, grid.cellSize));

        for (int i = 0; i < path.Count; i++)
        {
            Vector3 pos = GridUtility.GridToWorld(path[i].x, path[i].y, grid.cellSize);
            pos.y += 0.2f; // slightly above grid
            line.SetPosition(i + 1, pos);
        }
    }

    public void Clear()
    {
        line.positionCount = 0;
    }
}
