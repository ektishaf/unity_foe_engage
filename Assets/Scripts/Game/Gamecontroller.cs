using System.Collections;
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
    public GameObject[] playerUnitPrefabs;
    public GameObject[] aiUnitPrefabs;
    List<Unit> units = new List<Unit>();

    [Header("Cursor")]
    public CursorVisual cursorVisual;

    [Header("Overlays")]
    public OverlayVisual overlayVisual;

    [Header("Fog")]
    public FogVisual fogVisual;

    [Header("Path Preview")]
    public PathPreview pathPreview;
    Unit[,] unitMap;

    int activeTeam = 0;
    Unit selectedUnit = null;
    int selectedUnitIndex = -1;

    //ai
    Dictionary<Unit, Unit> aiTargetAssignments = new Dictionary<Unit, Unit>();
    void Awake()
    {
        settings = GameSettingsLoader.Settings;
        grid = new Grid(settings.gridWidth, settings.gridHeight, settings.cellSize);
        cursor = new Cursor(settings.gridWidth / 2, 0, grid);
        unitMap = new Unit[grid.width, grid.height];

        gridVisual.Init(grid);
        unitVisual.Init();
        cursorVisual.Init(cursor);
        overlayVisual.Init();
        fogVisual.Init(grid);
        pathPreview.Init(grid);
    }

    private void Start()
    {
        gridVisual.Build();
        SpawnUnits();
        overlayVisual.Init();
        fogVisual.Build();
        fogVisual.UpdateFog(units.ToArray());
        unitMap = BuildUnitMap(units, grid.width, grid.height);
        SelectFirstUnit();
    }

    public void SpawnUnits()
    {
        units.Clear();

        // Generate player units (Team 1)
        for (int i = 0; i < 4; i++)
        {
            Unit u = new Unit
            {
                unitName = "Player_" + (i + 1),
                team = 0,
                x = settings.gridWidth / 2 + i - 1,
                y = 0,
                movementPoints = 3,
                attackRange = 1,
                health = 10,
                attack = 5,
                defense = 2,
                hasActed = false,
            };
            units.Add(u);
        }

        // Generate AI units (Team 2)
        for (int i = 0; i < 4; i++)
        {
            Unit u = new Unit
            {
                unitName = "AI_" + (i + 1),
                team = 1,
                x = settings.gridWidth / 2 + i - 1,
                y = settings.gridHeight - 1,
                movementPoints = 3,
                attackRange = 1,
                health = 10 + i,
                attack = 5,
                defense = 2,
                hasActed = false,
                difficulty = AIDifficulty.Smart
            };
            units.Add(u);
        }

        List<GameObject> unitPrefabs = new List<GameObject>();
        unitPrefabs.AddRange(playerUnitPrefabs);
        unitPrefabs.AddRange(aiUnitPrefabs);
        unitVisual.SpawnUnits(units, unitPrefabs);
    }

    void Update()
    {
        HandleUnitSelection();
    }

    void HandleUnitSelection()
    {
        if (Input.GetKeyDown(settings.selectKey))
        {
            // select unit under cursor
            foreach (var unit in units)
            {
                if (unit.x == cursor.x && unit.y == cursor.y)
                {
                    selectedUnit = unit;
                    Debug.Log($"Selected Unit: {unit.unitName}, Team: {unit.team}");
                    //ShowUnitRanges(u);
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
                unitVisual.UpdateUnitPosition(selectedUnit);
                overlayVisual.Clear();
                fogVisual.UpdateFog(units.ToArray());
            }
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            //SwitchUnit();
            EndSelectedUnitTurn();
        }

        UpdatePathPreview();
    }

    public void ShowMovementRange(Unit u)
    {
        overlayVisual.Clear();
        var moveRange = MovementSystem.GetMoveRange(grid, u);
        overlayVisual.ShowMovement(moveRange);
    }

    public void ShowAttackRange(Unit u)
    {
        overlayVisual.Clear();
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

    public List<Unit> GetSelectableUnits()
    {
        List<Unit> list = new List<Unit>();

        foreach (var unit in units)
        {
            if (unit.team != activeTeam) continue;
            if (unit.hasActed) continue;

            list.Add(unit);
        }

        return list;
    }


    public void SwitchUnit()
    {
        List<Unit> selectable = GetSelectableUnits();
        Debug.Log($"selectable: {selectable.Count}");
        if (selectable.Count == 0)
        {
            EndPhase();
            return;
        }

        selectedUnitIndex = (selectedUnitIndex + 1) % selectable.Count;
        selectedUnit = selectable[selectedUnitIndex];

        cursor.SetPosition(selectedUnit.x, selectedUnit.y);
        ShowMovementRange(selectedUnit);
    }

    void SelectFirstUnit()
    {
        List<Unit> selectable = GetSelectableUnits();

        if (selectable.Count == 0)
        {
            EndPhase();
            return;
        }

        selectedUnitIndex = 0;
        selectedUnit = selectable[0];

        cursor.SetPosition(selectedUnit.x, selectedUnit.y);
        ShowMovementRange(selectedUnit);
    }

    void EndSelectedUnitTurn()
    {
        selectedUnit.hasActed = true;
        SwitchUnit(); // auto-advance
    }

    void EndPhase()
    {
        if (activeTeam == 0)
        {
            activeTeam = 1;
            StartAITurn();
        }
        else
        {
            Debug.Log("Player Turn");
            activeTeam = 0;
            ResetUnitsForNewTurn();
            SelectFirstUnit();
        }
    }
    void ResetUnitsForNewTurn()
    {
        foreach (var unit in units)
        {
            unit.hasActed = false;
        }
    }

    // AI
    IEnumerator RunAITurn()
    {
        Debug.Log("AI TURN START");

        // 🔒 Take a snapshot of AI units
        List<Unit> aiUnits = new List<Unit>();

        foreach (var u in units)
        {
            if (u.team == 1 && !u.hasActed)
            {
                aiUnits.Add(u);
            }
        }

        foreach (var ai in aiUnits)
        {
            // Unit might already be dead due to chain effects
            if (!units.Contains(ai)) continue;

            ExecuteAIMove(ai);
            yield return new WaitForSeconds(0.4f);

            ai.hasActed = true;
        }

        yield return new WaitForSeconds(0.3f);
        EndPhase();
    }


    void StartAITurn()
    {
        aiTargetAssignments.Clear();
        StartCoroutine(RunAITurn());
    }

    void ExecuteAIMove(Unit ai)
    {
        switch (ai.difficulty)
        {
            case AIDifficulty.Dumb:
                RandomMove(ai);
                break;

            case AIDifficulty.Normal:
                ExecuteAIMove_Normal(ai);
                break;

            case AIDifficulty.Smart:
                ExecuteAIMove_Smart(ai);
                break;
        }
    }

    Unit FindBestAvailableTarget(Unit ai)
    {
        Unit best = null;
        int bestScore = int.MinValue;

        foreach (var u in units)
        {
            if (u.team != 0) continue;

            int score = 0;

            // Prefer low HP
            score += (100 - u.health);

            // Prefer closer targets
            int dist = Mathf.Abs(ai.x - u.x) + Mathf.Abs(ai.y - u.y);
            score -= dist * 5;

            // Penalize already-targeted units
            int attackers = CountAttackers(u);
            score -= attackers * 30;

            if (score > bestScore)
            {
                bestScore = score;
                best = u;
            }
        }

        if (best != null)
            aiTargetAssignments[ai] = best;

        return best;
    }

    int CountAttackers(Unit target)
    {
        int count = 0;
        foreach (var pair in aiTargetAssignments)
        {
            if (pair.Value == target)
                count++;
        }
        return count;
    }


    Unit FindClosestPlayer(Unit ai)
    {
        Unit best = null;
        int bestDist = int.MaxValue;

        foreach (var u in units)
        {
            if (u.team != 0) continue;

            int d = Mathf.Abs(ai.x - u.x) + Mathf.Abs(ai.y - u.y);
            if (d < bestDist)
            {
                bestDist = d;
                best = u;
            }
        }

        return best;
    }

    void MoveUnit(Unit unit, Vector2Int pos)
    {
        grid.Get(unit.x, unit.y).occupied = false;

        unit.x = pos.x;
        unit.y = pos.y;

        grid.Get(unit.x, unit.y).occupied = true;

        unitVisual.UpdateUnitPosition(unit);
    }

    void ExecuteAttack(Unit attacker, Unit defender)
    {
        Debug.Log($"{attacker.unitName} attacks {defender.unitName}");

        defender.health -= Mathf.Max(1, attacker.attack - defender.defense);

        if (defender.health <= 0)
        {
            RemoveUnit(defender);
        }
    }
    void RemoveUnit(Unit u)
    {
        grid.Get(u.x, u.y).occupied = false;
        unitVisual.RemoveUnit(u);
        units.Remove(u);
    }

    void RandomMove(Unit ai)
    {
        // Try attack first
        foreach (var u in units)
        {
            if (u.team == 0)
            {
                int dist = Mathf.Abs(ai.x - u.x) + Mathf.Abs(ai.y - u.y);
                if (dist <= ai.attackRange)
                {
                    ExecuteAttack(ai, u);
                    ai.hasActed = true;
                    return;
                }
            }
        }

        // Random movement
        List<Vector2Int> possibleMoves =
            new List<Vector2Int>(MovementSystem.GetMoveRange(grid, ai));

        if (possibleMoves.Count > 0)
        {
            Vector2Int move =
                possibleMoves[Random.Range(0, possibleMoves.Count)];
            MoveUnit(ai, move);
        }

        ai.hasActed = true;
    }

    void ExecuteAIMove_Normal(Unit ai)
    {
        Unit target = FindClosestPlayer(ai);
        if (target == null)
        {
            ai.hasActed = true;
            return;
        }

        int dist = Mathf.Abs(ai.x - target.x) + Mathf.Abs(ai.y - target.y);

        // Attack if possible
        if (dist <= ai.attackRange)
        {
            ExecuteAttack(ai, target);
            ai.hasActed = true;
            return;
        }

        // Move toward target
        var path = AStarPathfinderAI.FindPath(
            grid,
            new Vector2Int(ai.x, ai.y),
            new Vector2Int(target.x, target.y)
        );

        if (path != null && path.Count > 0)
        {
            MoveUnit(ai, path[0]); // single-step movement
        }

        ai.hasActed = true;
    }

    void ExecuteAIMove_Smart(Unit ai)
    {
        Unit target = FindBestAvailableTarget(ai);
        if (target == null)
        {
            ai.hasActed = true;
            return;
        }

        int dist = Mathf.Abs(ai.x - target.x) + Mathf.Abs(ai.y - target.y);

        // Attack first
        if (dist <= ai.attackRange)
        {
            ExecuteAttack(ai, target);
            ai.hasActed = true;
            return;
        }

        // Pathfind smartly
        var path = AStarPathfinderAI.FindPath(
            grid,
            new Vector2Int(ai.x, ai.y),
            new Vector2Int(target.x, target.y)
        );

        if (path != null && path.Count > 0)
        {
            MoveUnit(ai, path[0]);
        }

        ai.hasActed = true;
    }

    Unit FindWeakestPlayer(Unit ai)
    {
        Unit best = null;
        int lowestHP = int.MaxValue;

        foreach (var u in units)
        {
            if (u.team != 0) continue;

            if (u.health < lowestHP)
            {
                lowestHP = u.health;
                best = u;
            }
        }

        return best;
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
