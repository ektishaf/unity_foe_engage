using UnityEngine;

public class GridVisual : MonoBehaviour
{
    [HideInInspector]
    public GridData gridData;
    private GameObject[,] tileObjects;

    private Transform container;
    private GameSettings settings;

    private void Awake()
    {
        settings = GameSettingsLoader.Settings;

        GameObject obj = new GameObject("CellContainer");
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
        container = obj.transform;
    }

    public void Build(GridData grid)
    {
        gridData = grid;
        tileObjects = new GameObject[grid.width, grid.height];
        
        for (int x = 0; x < grid.width; x++)
        {
            for (int y = 0; y < grid.height; y++)
            {
                Debug.Log($"{x} {y}");
                GameObject t = Instantiate(settings.cellPrefab, GridUtility.GridToWorld(x, y, grid.cellSize), Quaternion.identity, container);
                t.name = $"Cell({x}, {y})";

                var meshRenderer = t.GetComponentInChildren<MeshRenderer>();
                if(meshRenderer)
                {
                    meshRenderer.material.color = settings.cellColor;
                }

                tileObjects[x, y] = t;
            }
        }
    }

    public void HighlightTile(int x, int y, Color color)
    {
        if (gridData == null || x < 0 || y < 0 || x >= gridData.width || y >= gridData.height) return;

        var meshRenderer = tileObjects[x, y].GetComponentInChildren<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.material.color = color;
        }
    }

    public void ResetColors()
    {
        if (tileObjects == null) return;
        for (int x = 0; x < gridData.width; x++)
        {
            for (int y = 0; y < gridData.height; y++)
            {
                var meshRenderer = tileObjects[x, y].GetComponentInChildren<MeshRenderer>();
                if (meshRenderer != null)
                {
                    meshRenderer.material.color = settings.cellColor;
                }
            }
        }
    }
}
