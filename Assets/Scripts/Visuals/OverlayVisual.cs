using System.Collections.Generic;
using UnityEngine;

public class OverlayVisual : MonoBehaviour
{
    List<GameObject> activeTiles = new List<GameObject>();
    GridVisual gridVisual;
    Transform container;
    GameSettings settings;

    private void Awake()
    {
        settings = GameSettingsLoader.Settings;

        GameObject obj = new GameObject("OverlayContainer");
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
        container = obj.transform;
    }

    public void Initialize(GridVisual grid)
    {
        gridVisual = grid;
    }

    public void ShowMovement(HashSet<Vector2Int> positions)
    {
        Clear();
        foreach (var pos in positions)
        {
            GameObject t = Instantiate(settings.moveCellPrefab, GridUtility.GridToWorld(pos.x, pos.y, gridVisual.gridData.cellSize) + Vector3.up * 0.05f, Quaternion.identity, container);
            var meshRenderer = t.GetComponentInChildren<MeshRenderer>();
            if (meshRenderer)
            {
                meshRenderer.material.color = settings.moveCellColor;
            }
            activeTiles.Add(t);
        }
    }

    public void ShowAttack(HashSet<Vector2Int> positions)
    {
        Clear();
        foreach (var pos in positions)
        {
            GameObject t = Instantiate(settings.attackCellPrefab, GridUtility.GridToWorld(pos.x, pos.y, gridVisual.gridData.cellSize) + Vector3.up * 0.05f, Quaternion.identity, container);
            var meshRenderer = t.GetComponentInChildren<MeshRenderer>();
            if (meshRenderer)
            {
                meshRenderer.material.color = settings.attackCellColor;
            }
            activeTiles.Add(t);
        }
    }

    public void Clear()
    {
        foreach (var go in activeTiles)
            Destroy(go);
        activeTiles.Clear();
    }
}
