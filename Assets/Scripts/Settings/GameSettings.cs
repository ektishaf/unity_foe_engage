using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings",menuName = "Foe Engage/Game Settings")]
public class GameSettings : ScriptableObject
{
    [Header("Grid")]
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float cellSize = 1f;
    public GameObject cellPrefab;
    public Color cellColor = Color.grey;

    [Header("Cursor")]
    public GameObject cursorCellPrefab;
    public Color cursorCellColor = Color.yellow;

    [Header("Movement")]
    public GameObject moveCellPrefab;
    public Color moveCellColor = Color.green;
    public float moveAnimDuration = 0.2f;

    [Header("Attack")]
    public GameObject attackCellPrefab;
    public Color attackCellColor = Color.red;

    [Header("Units")]
    public GameObject defaultUnitPrefab;
    public int defaultMovementPoints = 3;
    public int defaultAttackRange = 1;
    public int defaultVisionRange = 3;

    [Header("Fog")]
    public GameObject fogCellPrefab;
    public Color fogCellColor = Color.olive;

    [Header("Teams")]
    public int unitsPerTeam = 3;

    [Header("Input")]
    public KeyCode selectKey = KeyCode.Space;
    public KeyCode moveKey = KeyCode.M;
}
