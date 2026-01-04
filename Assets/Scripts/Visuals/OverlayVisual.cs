using System.Collections.Generic;
using UnityEngine;

public class OverlayVisual : MonoBehaviour
{
    GameSettings settings;
    Transform container;
    GridVisual gridVisual;
    List<GameObject> visuals = new List<GameObject>();
    
    public void Init()
    {
        settings = GameSettingsLoader.Settings;

        container = new GameObject("OverlayContainer").transform;
        container.position = Vector3.zero;
        container.rotation = Quaternion.identity;
    }

    public void ShowMovement(HashSet<Vector2Int> positions)
    {
        foreach (var pos in positions)
        {
            GameObject visual = Instantiate(settings.moveCellPrefab, GridUtility.GridToWorld(pos.x, pos.y, settings.cellSize) + Vector3.up * 0.05f, Quaternion.identity, container);
            visuals.Add(visual);
        }
    }

    public void ShowAttack(HashSet<Vector2Int> positions)
    {
        foreach (var pos in positions)
        {
            GameObject visual = Instantiate(settings.attackCellPrefab, GridUtility.GridToWorld(pos.x, pos.y, settings.cellSize) + Vector3.up * 0.05f, Quaternion.identity, container);
            visuals.Add(visual);
        }
    }

    public void Clear()
    {
        foreach (var visual in visuals)
        {
            Destroy(visual);
        }
        visuals.Clear();
    }
}