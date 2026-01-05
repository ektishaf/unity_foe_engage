using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CursorVisual : MonoBehaviour
{
    GameSettings settings;
    Transform visual;
    Cursor cursor;
    
    public void Init(Cursor cursor)
    {
        settings = GameSettingsLoader.Settings;
        this.cursor = cursor;

        visual = Instantiate(settings.cursorCellPrefab, Vector3.zero, Quaternion.identity).transform;
        visual.position = Vector3.zero;
        visual.rotation = Quaternion.identity;

        UpdatePosition();
    }

    void UpdatePosition()
    {
        visual.position = GridUtility.GridToWorld(cursor.x, cursor.y, cursor.GridRef.cellSize) + new Vector3(0, 0.2f, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            cursor.Move(0, 1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            cursor.Move(0, -1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        { 
            cursor.Move(-1, 0); 
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) 
        { 
            cursor.Move(1, 0); 
        }

        UpdatePosition();
    }
}
