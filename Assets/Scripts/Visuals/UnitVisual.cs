using System.Collections.Generic;
using UnityEngine;

public class UnitVisual : MonoBehaviour
{
    GameSettings settings;
    Transform container;
    Grid grid;
    Dictionary<Unit, GameObject> visuals = new Dictionary<Unit, GameObject>();
    
    public void Init(Grid grid)
    {
        settings = GameSettingsLoader.Settings;
        this.grid = grid;

        GameObject obj = new GameObject("UnitContainer");
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
        container = obj.transform;
    }

    public void SpawnUnit(Unit unit, GameObject prefab = null)
    {
        if (visuals.ContainsKey(unit)) return;

        if (prefab == null) prefab = settings.defaultUnitPrefab;
        if (prefab == null)
        {
            Debug.LogError("No prefab assigned for unit: " + unit.unitName);
            return;
        }

        Vector3 position = grid.GridToWorld(unit);
        GameObject visual = Instantiate(prefab, position, Quaternion.identity, container);
        visual.name = unit.unitName;
        visuals[unit] = visual;

        if(unit.visual == null)
        {
            unit.visual = visual.transform;
        }
    }

    public void SpawnUnits(List<Unit> units, List<GameObject> unitPrefabs)
    {
        ClearAll();

        for(int i = 0; i < units.Count; i++)
        {
            SpawnUnit(units[i], unitPrefabs[i]);
        }
    }

    public void UpdateUnitPosition(Unit unit)
    {
        if (!visuals.TryGetValue(unit, out GameObject visual)) return;

        visual.transform.position = grid.GridToWorld(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        if (!visuals.TryGetValue(unit, out GameObject visual)) return;

        Destroy(visual);
        visuals.Remove(unit);
    }

    public void ClearAll()
    {
        foreach (var visual in visuals)
        {
            if (visual.Value != null)
            {
                Destroy(visual.Value);
            }
        }
        visuals.Clear();
    }
}
