using UnityEngine;

//[ExecuteAlways]
public class GridGizmoDrawer : MonoBehaviour
{
    [Header("Grid Settings")]
    public int width = 10;
    public int height = 10;
    public float cellSize = 1f;

    [Header("Gizmo Settings")]
    public Color gridColor = new Color(1f, 1f, 1f, 1f);
    public float yOffset = 0.01f;

    private void OnDrawGizmos()
    {
        Gizmos.color = gridColor;

        Vector3 origin = transform.position;

        // Vertical lines (X direction)
        for (int x = 0; x <= width; x++)
        {
            Vector3 start = origin + new Vector3(x * cellSize, yOffset, 0);
            Vector3 end = origin + new Vector3(x * cellSize, yOffset, height * cellSize);
            Gizmos.DrawLine(start, end);
        }

        // Horizontal lines (Z direction)
        for (int y = 0; y <= height; y++)
        {
            Vector3 start = origin + new Vector3(0, yOffset, y * cellSize);
            Vector3 end = origin + new Vector3(width * cellSize, yOffset, y * cellSize);
            Gizmos.DrawLine(start, end);
        }
    }
}
