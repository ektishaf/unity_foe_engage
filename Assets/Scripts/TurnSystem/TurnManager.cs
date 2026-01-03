using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public List<Unit> units = new List<Unit>();
    private int index = 0;

    public Unit ActiveUnit => units.Count > 0 ? units[index] : null;

    public void Register(Unit u)
    {
        if (!units.Contains(u))
            units.Add(u);
    }

    public void EndTurn()
    {
        index = (index + 1) % units.Count;
        Debug.Log($"Turn: {ActiveUnit.unitName} | Team {ActiveUnit.team}");
    }
}
