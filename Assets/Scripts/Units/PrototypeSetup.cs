using System.Collections.Generic;
using UnityEngine;

public class PrototypeSetup : MonoBehaviour
{
    GameSettings settings;

    public UnitVisual unitVisual;
    public GameObject[] playerUnitPrefabs;
    public GameObject[] aiUnitPrefabs;

    [HideInInspector]
    public List<Unit> units = new List<Unit>();

    public void Init()
    {
        settings = GameSettingsLoader.Settings;
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
                team = 1,
                x = settings.gridWidth / 2 + i - 1, // spread near center
                y = 0,                  // bottom row
                movementPoints = 3,
                attackRange = 1,
                health = 10,
                attack = 5,
                defense = 2
            };
            units.Add(u);
        }

        // Generate AI units (Team 2)
        for (int i = 0; i < 4; i++)
        {
            Unit u = new Unit
            {
                unitName = "AI_" + (i + 1),
                team = 2,
                x = settings.gridWidth / 2 + i - 1,
                y = settings.gridHeight - 1,
                movementPoints = 3,
                attackRange = 1,
                health = 10,
                attack = 5,
                defense = 2
            };
            units.Add(u);
        }

        List<GameObject> unitPrefabs = new List<GameObject>();
        unitPrefabs.AddRange(playerUnitPrefabs);
        unitPrefabs.AddRange(aiUnitPrefabs);
        unitVisual.SpawnUnits(units, unitPrefabs);
    }
}
