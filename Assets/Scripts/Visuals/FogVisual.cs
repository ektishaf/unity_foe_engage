using UnityEngine;

public class FogVisual : MonoBehaviour
{
    GameSettings settings;
    Transform container;
    Grid grid;
    GameObject[,] visuals;
    
    public void Init(Grid grid)
    {
        settings = GameSettingsLoader.Settings;
        this.grid = grid;

        container = new GameObject("FogContainer").transform;
        container.position = Vector3.zero;
        container.rotation = Quaternion.identity;
    }

    public void Build()
    {
        visuals = new GameObject[grid.width, grid.height];

        for (int x = 0; x < grid.width; x++)
        {
            for (int y = 0; y < grid.height; y++)
            {
                GameObject visual = Instantiate(settings.fogCellPrefab, GridUtility.GridToWorld(x, y, grid.cellSize), Quaternion.identity, container);
                visual.name = $"Fog_{x}_{y}";
                visuals[x, y] = visual;
            }
        }
    }

    public void UpdateFog(Unit[] units)
    {
        // first, cover everything
        for (int x = 0; x < grid.width; x++)
        {
            for (int y = 0; y < grid.height; y++)
            {
                visuals[x, y].SetActive(true);
            }
        }
        // then uncover tiles within each unit's vision range
        foreach (var u in units)
        {
            int range = u.visionRange;
            for (int dx = -range; dx <= range; dx++)
            {
                for (int dy = -range; dy <= range; dy++)
                {
                    int nx = u.x + dx;
                    int ny = u.y + dy;
                    if (grid.Inside(nx, ny))
                    {
                        visuals[nx, ny].SetActive(false);
                    }
                }
            }
        }
    }
}
