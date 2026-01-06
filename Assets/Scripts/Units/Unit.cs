using UnityEngine;

public class Unit
{
    public string unitName;
    public int team;
    public int x, y;
    public int movementPoints;
    public int attackRange;
    public int health;
    public int attack;
    public int defense;
    public int visionRange = 1;
    public bool hasActed;
    public Transform visual;
    public AIDifficulty difficulty;

    public Vector2Int GridPos => new Vector2Int(x, y);
}
