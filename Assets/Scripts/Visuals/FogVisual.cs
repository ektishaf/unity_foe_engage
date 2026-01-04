using UnityEngine;

public class FogVisual : MonoBehaviour
{
    GameObject[,] fogTiles;
    GridData gridData;
    Transform container;
    GameSettings settings;

    private void Awake()
    {
        settings = GameSettingsLoader.Settings;

        GameObject obj = new GameObject("FogContainer");
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
        container = obj.transform;
    }

    public void Build(GridData grid)
    {
        gridData = grid;
        fogTiles = new GameObject[grid.width, grid.height];

        for (int x = 0; x < grid.width; x++)
        {
            for (int y = 0; y < grid.height; y++)
            {
                GameObject f = Instantiate(settings.fogCellPrefab, GridUtility.GridToWorld(x, y, grid.cellSize), Quaternion.identity, container);
                f.name = $"Fog_{x}_{y}";
                var meshRenderer = f.GetComponentInChildren<MeshRenderer>();
                if (meshRenderer)
                {
                    meshRenderer.material.color = settings.fogCellColor;
                }
                fogTiles[x, y] = f;
            }
        }
    }

    /// <summary>
    /// Updates fog based on units' vision
    /// </summary>
    public void UpdateFog(Unit[] units)
    {
        // first, cover everything
        for (int x = 0; x < gridData.width; x++)
        {
            for (int y = 0; y < gridData.height; y++)
            {
                fogTiles[x, y].SetActive(true);
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
                    if (gridData.Inside(nx, ny))
                    {
                        fogTiles[nx, ny].SetActive(false);
                    }
                }
            }
        }
    }
}
