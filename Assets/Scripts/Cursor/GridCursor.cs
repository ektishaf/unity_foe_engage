using UnityEngine;

public class GridCursor : MonoBehaviour
{
    public GridManager grid;

    public int x;
    public int y;

    void Start()
    {
        UpdatePosition();
    }

    void Update()
    {
        int dx = 0, dy = 0;

        if (Input.GetKeyDown(KeyCode.UpArrow)) dy = 1;
        if (Input.GetKeyDown(KeyCode.DownArrow)) dy = -1;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) dx = -1;
        if (Input.GetKeyDown(KeyCode.RightArrow)) dx = 1;

        int nx = x + dx;
        int ny = y + dy;

        if (!grid.Inside(nx, ny)) return;

        x = nx;
        y = ny;
        UpdatePosition();
    }

    void UpdatePosition()
    {
        transform.position = grid.GridToWorld(x, y);
    }
}
