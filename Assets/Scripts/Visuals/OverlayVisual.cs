using System.Collections.Generic;
using UnityEngine;

public class OverlayVisual : MonoBehaviour
{
    GameSettings settings;
    Transform container;
    Grid grid;
    List<GameObject> visuals = new List<GameObject>();
    
    public void Init(Grid grid)
    {
        settings = GameSettingsLoader.Settings;
        this.grid = grid;

        container = new GameObject("OverlayContainer").transform;
        container.position = Vector3.zero;
        container.rotation = Quaternion.identity;
    }

    public void ShowMovement(HashSet<Vector2Int> positions)
    {
        foreach (var pos in positions)
        {
            GameObject visual = Instantiate(settings.moveCellPrefab, grid.GridToWorld(pos.x, pos.y) + Vector3.up * 0.05f, Quaternion.identity, container);
            visuals.Add(visual);
        }
    }

    public void ShowAttack(HashSet<Vector2Int> positions)
    {
        foreach (var pos in positions)
        {
            GameObject visual = Instantiate(settings.attackCellPrefab, grid.GridToWorld(pos.x, pos.y) + Vector3.up * 0.05f, Quaternion.identity, container);
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