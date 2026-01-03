using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    public GridManager grid;
    public GameObject fogPrefab;
    public int visionRange = 3;

    GameObject[,] fog;

    void Awake()
    {
        fog = new GameObject[grid.width, grid.height];

        for (int x = 0; x < grid.width; x++)
        {
            for (int y = 0; y < grid.height; y++)
            {
                GameObject f = Instantiate(fogPrefab);
                f.transform.position = grid.GridToWorld(x, y);
                fog[x, y] = f;
            }
        }
    }

    public void UpdateFog(Unit[] units)
    {
        for (int x = 0; x < grid.width; x++)
        {
            for (int y = 0; y < grid.height; y++)
            {
                fog[x, y].SetActive(true);
            }
        }

        foreach (var u in units)
        {
            for (int dx = -visionRange; dx <= visionRange; dx++)
            {
                for (int dy = -visionRange; dy <= visionRange; dy++)
                {
                    int nx = u.x + dx;
                    int ny = u.y + dy;
                    if (grid.Inside(nx, ny))
                    {
                        fog[nx, ny].SetActive(false);
                    }
                }
            }
        }
    }
}
