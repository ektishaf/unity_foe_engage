using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings",menuName = "Foe Engage/Game Settings")]
public class GameSettings : ScriptableObject
{
    [Header("Grid")]
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float cellSize = 1f;
    public GameObject cellPrefab;

    [Header("Cursor")]
    public GameObject cursorCellPrefab;

    [Header("Movement")]
    public GameObject moveCellPrefab;
    public float moveAnimDuration = 0.2f;

    [Header("Attack")]
    public GameObject attackCellPrefab;

    [Header("Units")]
    public GameObject defaultUnitPrefab;
    public int defaultMovementPoints = 3;
    public int defaultAttackRange = 1;
    public int defaultVisionRange = 3;

    [Header("Fog")]
    public GameObject fogCellPrefab;

    [Header("Teams")]
    public int unitsPerTeam = 3;

    [Header("Input")]
    public KeyCode selectKey = KeyCode.Space;
    public KeyCode moveKey = KeyCode.M;
}
