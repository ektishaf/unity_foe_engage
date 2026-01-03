using System.Collections.Generic;
using UnityEngine;

public class VisualGridHighlighter : MonoBehaviour
{
    public GameObject highlightPrefab;
    public Transform container;

    private List<GameObject> pool = new List<GameObject>();

    public void ShowMovementRange(HashSet<Vector2Int> cells, GridManager grid)
    {
        Clear();

        foreach (var cell in cells)
        {
            GameObject h = GetHighlight();
            h.transform.position = grid.GridToWorld(cell.x, cell.y);
            h.transform.localScale = Vector3.one * grid.cellSize;
            h.GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    public void ShowAttackRange(HashSet<Vector2Int> cells, GridManager grid)
    {
        Clear();

        foreach (var cell in cells)
        {
            GameObject h = GetHighlight();
            h.transform.position = grid.GridToWorld(cell.x, cell.y);
            h.transform.localScale = Vector3.one * grid.cellSize;
            h.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    GameObject GetHighlight()
    {
        foreach (var go in pool)
            if (!go.activeSelf)
            {
                go.SetActive(true);
                return go;
            }

        GameObject newH = Instantiate(highlightPrefab, container);
        pool.Add(newH);
        return newH;
    }

    public void Clear()
    {
        foreach (var go in pool)
            go.SetActive(false);
    }
}
