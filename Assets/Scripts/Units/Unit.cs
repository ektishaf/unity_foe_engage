using UnityEngine;

public class Unit
{
    public string unitName;
    public int team;
    public int x, y;                 // Grid position
    public int movementPoints;       // How far it can move
    public int attackRange;          // Tiles it can attack
    public int health;
    public int attack;
    public int defense;
    public int visionRange = 1;
    public bool hasActed;
    public Transform visual;         // optional for rendering

    public Vector2Int GridPos => new Vector2Int(x, y);
}
