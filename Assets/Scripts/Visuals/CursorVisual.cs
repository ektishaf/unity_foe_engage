using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CursorVisual : MonoBehaviour
{
    public GridVisual gridVisual;
    public Cursor cursorData;

    private Transform cursor;
    private GameSettings settings;

    private void Awake()
    {
        settings = GameSettingsLoader.Settings;

        GameObject obj = Instantiate(settings.cursorCellPrefab, Vector3.zero, Quaternion.identity);
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
        var meshRenderer = obj.GetComponentInChildren<MeshRenderer>();
        if (meshRenderer)
        {
            meshRenderer.material.color = settings.cursorCellColor;
        }
        cursor = obj.transform;
    }

    public void Initialize(Cursor cursor, GridVisual grid)
    {
        cursorData = cursor;
        gridVisual = grid;
        UpdatePosition();
    }

    void UpdatePosition()
    {
        cursor.position = GridUtility.GridToWorld(cursorData.x, cursorData.y, gridVisual.gridData.cellSize) + new Vector3(0, 0.2f, 0);
    }

    void Update()
    {
        // move cursor with arrow keys
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            cursorData.Move(0, 1, gridVisual.gridData);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            cursorData.Move(0, -1, gridVisual.gridData);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        { 
            cursorData.Move(-1, 0, gridVisual.gridData); 
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) 
        { 
            cursorData.Move(1, 0, gridVisual.gridData); 
        }

        UpdatePosition();
    }
}
