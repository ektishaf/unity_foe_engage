using System.Collections.Generic;
using UnityEngine;

public class GridVisualizer : MonoBehaviour
{
    public GridManager grid;
    public GameObject tilePrefab;

    List<GameObject> pool = new List<GameObject>();

    void Clear()
    {
        foreach (var t in pool) t.SetActive(false);
    }

    GameObject GetTile()
    {
        foreach (var t in pool)
            if (!t.activeSelf)
            {
                t.SetActive(true);
                return t;
            }

        GameObject go = Instantiate(tilePrefab);
        pool.Add(go);
        return go;
    }

    public void Show(HashSet<Vector2Int> cells, Color color)
    {
        Clear();
        foreach (var c in cells)
        {
            GameObject t = GetTile();
            t.transform.position = grid.GridToWorld(c.x, c.y);
            t.GetComponent<Renderer>().material.color = color;
        }
    }

    public void ShowPath(List<Vector2Int> path)
    {
        foreach (var p in path)
        {
            GameObject t = GetTile();
            t.transform.position = grid.GridToWorld(p.x, p.y);
            t.GetComponent<Renderer>().material.color = Color.cyan;
        }
    }

    void OnDrawGizmos()
    {
        if (grid == null) return;
        Gizmos.color = Color.gray;

        for (int x = 0; x < grid.width; x++)
            for (int y = 0; y < grid.height; y++)
            {
                Gizmos.DrawWireCube(
                    grid.GridToWorld(x, y),
                    new Vector3(grid.cellSize, 0.01f, grid.cellSize));
            }
    }
}
