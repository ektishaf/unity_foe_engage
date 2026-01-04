using UnityEngine;

[ExecuteAlways]
public class GridGizmoDrawer : MonoBehaviour
{
    GameSettings settings;

    private void OnDrawGizmos()
    {
        if (settings == null) settings = GameSettingsLoader.Settings;
        Gizmos.color = settings.cellColor;

        Vector3 origin = transform.position;

        // Vertical lines (X direction)
        for (int x = 0; x <= settings.gridWidth; x++)
        {
            Vector3 start = origin + new Vector3(x * settings.cellSize, 0.1f, 0);
            Vector3 end = origin + new Vector3(x * settings.cellSize, 0.1f, settings.gridHeight * settings.cellSize);
            Gizmos.DrawLine(start, end);
        }

        // Horizontal lines (Z direction)
        for (int y = 0; y <= settings.gridHeight; y++)
        {
            Vector3 start = origin + new Vector3(0, 0.1f, y * settings.cellSize);
            Vector3 end = origin + new Vector3(settings.gridWidth * settings.cellSize, 0.1f, y * settings.cellSize);
            Gizmos.DrawLine(start, end);
        }
    }
}