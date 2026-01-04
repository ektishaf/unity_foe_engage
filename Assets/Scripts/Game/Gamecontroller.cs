using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Grid")]
    public GridVisual gridVisual;
    public GridData gridData;

    [Header("Units")]
    public UnitVisual unitVisual;
    public List<Unit> units = new List<Unit>();

    [Header("Cursor")]
    public CursorVisual cursorVisual;

    [Header("Overlays")]
    public OverlayVisual overlayVisual;

    [Header("Fog")]
    public FogVisual fogVisual;

    [Header("Path Preview")]
    public PathPreview pathPreview;
    Unit[,] unitMap;

    private Cursor cursor;
    private GameSettings settings;

    void Awake()
    {
        settings = GameSettingsLoader.Settings;
        gridData = new GridData(settings.gridWidth, settings.gridHeight, settings.cellSize);
        cursor = new Cursor(settings.gridWidth / 2, 0);
        unitMap = new Unit[gridData.width, gridData.height];
    }

    private void Start()
    {
   
        gridVisual.Build(gridData);
        SpawnUnits();
        unitVisual.SpawnUnits(units);
        fogVisual.Build(gridData);
        fogVisual.UpdateFog(units.ToArray());
        cursorVisual.Initialize(cursor, gridVisual);
        overlayVisual.Initialize(gridVisual);
    }

    void Update()
    {
        HandleUnitSelection();
    }

    private Unit selectedUnit = null;

    void HandleUnitSelection()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // select unit under cursor
            foreach (var u in units)
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
            if (Input.GetKeyDown(KeyCode.M))
            {
                selectedUnit.x = cursor.x;
                selectedUnit.y = cursor.y;
                unitVisual.UpdateUnitPosition(selectedUnit);
                overlayVisual.Clear();
                fogVisual.UpdateFog(units.ToArray());
            }
        }

        UpdatePathPreview();
    }

    void ShowUnitRanges(Unit u)
    {
        var moveRange = MovementSystem.GetMoveRange(gridData, u);
        overlayVisual.ShowMovement(moveRange);
        Debug.Log($"Move Range: {moveRange.Count}");
        //var attackRange = MovementSystem.GetAttackRange(gridData, u);
        //Debug.Log($"Attack Range: {attackRange.Count}");
        //overlayVisual.ShowAttack(attackRange);
    }

    void SpawnUnits()
    {
        units.Clear();

        // Team 1: Bottom center
        for (int i = 0; i < settings.unitsPerTeam; i++)
        {
            Unit u = new Unit
            {
                unitName = $"Unit_T1_{i + 1}",
                team = 1,
                x = settings.gridWidth / 2,
                y = i,
                movementPoints = 3,
                attackRange = 5,
                health = 10,
                attack = 5,
                defense = 2
            };
            units.Add(u);
            unitMap[u.x, u.y] = u;
        }

        // Team 2: Top center
        for (int i = 0; i < settings.unitsPerTeam; i++)
        {
            Unit u = new Unit
            {
                unitName = $"Unit_T2_{i + 1}",
                team = 2,
                x = settings.gridWidth / 2,
                y = settings.gridHeight - 1 - i,
                movementPoints = 3,
                attackRange = 1,
                health = 10,
                attack = 5,
                defense = 2
            };
            units.Add(u);
            unitMap[u.x, u.y] = u;
        }
    }

    void UpdatePathPreview()
    {
        if (selectedUnit == null)
        {
            pathPreview.Clear();
            return;
        }

        List<Vector2Int> path = PathFinder.FindPathAStar(selectedUnit, new Vector2Int(cursor.x, cursor.y),gridData, unitMap);

        // Optional: limit by movement points
        if (path.Count > selectedUnit.movementPoints)
        {
            pathPreview.Clear();
            return;
        }

        pathPreview.DrawPath(path);
    }

    public static Unit[,] BuildUnitMap(
    List<Unit> units,
    int width,
    int height)
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
