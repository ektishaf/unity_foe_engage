using System.Collections.Generic;
using UnityEngine;

public class UnitVisual : MonoBehaviour
{
     Dictionary<Unit, GameObject> unitObjects = new Dictionary<Unit, GameObject>();

    private Transform container;
    private GameSettings settings;
    private void Awake()
    {
        settings = GameSettingsLoader.Settings;

        GameObject obj = new GameObject("UnitContainer");
        obj.transform.position = Vector3.zero;
        obj.transform.rotation = Quaternion.identity;
        container = obj.transform;
    }

    public void SpawnUnits(List<Unit> units)
    {
        foreach (var u in units)
        {
            GameObject go = Instantiate(settings.defaultUnitPrefab, GridUtility.GridToWorld(u, 1f), Quaternion.identity, container);
            go.name = u.unitName;
            unitObjects[u] = go;

            if (u.visual == null)
                u.visual = go.transform;
        }
    }

    public void UpdateUnitPosition(Unit unit)
    {
        if (unitObjects.ContainsKey(unit))
            unitObjects[unit].transform.position = GridUtility.GridToWorld(unit, 1f);
    }
}
