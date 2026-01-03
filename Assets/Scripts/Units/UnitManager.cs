using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public GridManager grid;
    public GameObject unitPrefab;

    public Unit[,] UnitMap { get; private set; }

    void Awake()
    {
        UnitMap = new Unit[grid.width, grid.height];
    }

    public Unit SpawnUnit(int x, int y, string name, int team)
    {
        if (!grid.Inside(x, y)) return null;
        if (UnitMap[x, y] != null) return null;

        GameObject go = Instantiate(unitPrefab);
        go.transform.position = grid.GridToWorld(x, y);

        Unit u = new Unit
        {
            unitName = name,
            team = team,
            x = x,
            y = y,
            visual = go.transform
        };

        UnitMap[x, y] = u;
        return u;
    }
}

