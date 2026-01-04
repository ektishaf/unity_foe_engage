using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    GameSettings settings;
    Cursor cursor;

    [Header("Grid")]
    public GridVisual gridVisual;
    public Grid grid;

    [Header("Unit")]
    public UnitVisual unitVisual;

    [Header("Cursor")]
    public CursorVisual cursorVisual;

    [Header("Overlays")]
    public OverlayVisual overlayVisual;

    [Header("Fog")]
    public FogVisual fogVisual;

    [Header("Path Preview")]
    public PathPreview pathPreview;
    Unit[,] unitMap;

    [Header("Prototype")]
    public PrototypeSetup prototypeSetup;

    void Awake()
    {
        settings = GameSettingsLoader.Settings;
        grid = new Grid(settings.gridWidth, settings.gridHeight, settings.cellSize);
        cursor = new Cursor(settings.gridWidth / 2, 0);
        unitMap = new Unit[grid.width, grid.height];

        gridVisual.Init(grid);
        unitVisual.Init();
        cursorVisual.Init(grid, cursor);
        overlayVisual.Init();
        fogVisual.Init(grid);
        prototypeSetup.Init();
        pathPreview.Init(grid);
    }

    private void Start()
    {
        gridVisual.Build();
        prototypeSetup.SpawnUnits();
        overlayVisual.Init();
        fogVisual.Build();
        fogVisual.UpdateFog(prototypeSetup.units.ToArray());
        unitMap = BuildUnitMap(prototypeSetup.units, grid.width, grid.height);
    }

    void Update()
    {
        HandleUnitSelection();
    }

    private Unit selectedUnit = null;

    void HandleUnitSelection()
    {
        if (Input.GetKeyDown(settings.selectKey))
        {
            // select unit under cursor
            foreach (var u in prototypeSetup.units)
            {
                if (u.x == cursor.x && u.y == cursor.y)
                {
                    selectedUnit = u;
                    Debug.Log($"Selected Unit: {u.unitName}, Team: {u.team}");
                    ShowUnitRanges(u);
                    return;
                }
            }
            selectedUnit = null;
            overlayVisual.Clear();
        }

        if (selectedUnit != null)
        {
            // move unit with cursor and M key
            if (Input.GetKeyDown(settings.moveKey))
            {
                selectedUnit.x = cursor.x;
                selectedUnit.y = cursor.y;
                prototypeSetup.unitVisual.UpdateUnitPosition(selectedUnit);
                overlayVisual.Clear();
                fogVisual.UpdateFog(prototypeSetup.units.ToArray());
            }
        }

        UpdatePathPreview();
    }

    void ShowUnitRanges(Unit u)
    {
        overlayVisual.Clear();
        var moveRange = MovementSystem.GetMoveRange(grid, u);
        overlayVisual.ShowMovement(moveRange);
        
        var attackRange = MovementSystem.GetAttackRange(grid, u);
        overlayVisual.ShowAttack(attackRange);
    }

    void UpdatePathPreview()
    {
        if (selectedUnit == null)
        {
            pathPreview.Clear();
            return;
        }

        List<Vector2Int> path = PathFinder.FindPathAStar(selectedUnit, new Vector2Int(cursor.x, cursor.y), grid, unitMap);

        // Optional: limit by movement points
        if (path.Count > selectedUnit.movementPoints)
        {
            pathPreview.Clear();
            return;
        }

        pathPreview.DrawPath(path);
    }

    public static Unit[,] BuildUnitMap(List<Unit> units, int width, int height)
    {
        Unit[,] unitMap = new Unit[width, height];

        foreach (Unit u in units)
        {
            if (u.x < 0 || u.x >= width ||
                u.y < 0 || u.y >= height)
            {
                Debug.LogError($"Unit {u.unitName} out of bounds");
                continue;
            }

            if (unitMap[u.x, u.y] != null)
            {
                Debug.LogError($"Cell occupied at {u.x},{u.y}");
                continue;
            }

            unitMap[u.x, u.y] = u;
        }

        return unitMap;
    }

}
