using UnityEngine;

public class GridVisual : MonoBehaviour
{
    GameSettings settings;
    Transform container;
    Grid grid;
    GameObject[,] visuals;

    public void Init(Grid grid)
    {
        settings = GameSettingsLoader.Settings;
        this.grid = grid;

        GameObject obj = new GameObject("CellContainer");
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
        container = obj.transform;
    }

    public void Build()
    {
        visuals = new GameObject[grid.width, grid.height];
        
        for (int x = 0; x < grid.width; x++)
        {
            for (int y = 0; y < grid.height; y++)
            {
                GameObject visual = Instantiate(settings.cellPrefab, GridUtility.GridToWorld(x, y, grid.cellSize), Quaternion.identity, container);
                visual.name = $"Cell({x}, {y})";
                visuals[x, y] = visual;
            }
        }
    }
}
