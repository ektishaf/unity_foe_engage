using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CursorVisual : MonoBehaviour
{
    GameSettings settings;
    Transform visual;
    Grid grid;
    Cursor cursor;
    
    public void Init(Grid grid, Cursor cursor)
    {
        settings = GameSettingsLoader.Settings;
        this.grid = grid;
        this.cursor = cursor;

        visual = Instantiate(settings.cursorCellPrefab, Vector3.zero, Quaternion.identity).transform;
        visual.position = Vector3.zero;
        visual.rotation = Quaternion.identity;
        //visual = obj.transform;

        UpdatePosition();
    }

    void UpdatePosition()
    {
        if (visual == null) return;
        if(cursor == null) return;
        if(grid == null) return;

        visual.position = GridUtility.GridToWorld(cursor.x, cursor.y, grid.cellSize) + new Vector3(0, 0.2f, 0);
    }

    void Update()
    {
        // move cursor with arrow keys
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            cursor.Move(0, 1, grid);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            cursor.Move(0, -1, grid);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        { 
            cursor.Move(-1, 0, grid); 
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) 
        { 
            cursor.Move(1, 0, grid); 
        }

        UpdatePosition();
    }
}
