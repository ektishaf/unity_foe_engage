using UnityEngine;
using System.Collections.Generic;

public class TacticalGame : MonoBehaviour
{
    public GridManager grid;
    public UnitManager units;
    public GridCursor cursor;
    public GridVisualizer visualizer;
    public TurnManager turns;
    public FogOfWar fog;

    void Start()
    {
        Unit a = units.SpawnUnit(2, 2, "Alpha", 0);
        Unit b = units.SpawnUnit(6, 6, "Bravo", 1);

        turns.Register(a);
        turns.Register(b);

        fog.UpdateFog(turns.units.ToArray());
    }

    void Update()
    {
        Unit active = turns.ActiveUnit;
        if (active == null) return;

        var moveRange = MovementRange.Calculate(active, grid, units.UnitMap);
        visualizer.Show(moveRange, Color.blue);

        Vector2Int cursorPos = new Vector2Int(cursor.x, cursor.y);
        if (moveRange.Contains(cursorPos))
        {
            var path = PathFinder.FindPath(active, cursorPos, grid, units.UnitMap);
            visualizer.ShowPath(path);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            units.UnitMap[active.x, active.y] = null;
            active.x = cursor.x;
            active.y = cursor.y;
            units.UnitMap[active.x, active.y] = active;
            active.visual.position = grid.GridToWorld(active.x, active.y);

            Debug.Log($"Moved {active.unitName} | Team {active.team}");
            turns.EndTurn();
            fog.UpdateFog(turns.units.ToArray());
        }
    }
}
