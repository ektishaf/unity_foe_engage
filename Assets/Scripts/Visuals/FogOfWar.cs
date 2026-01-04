using System.Collections.Generic;
using UnityEngine;

public static class FogSystem
{
    public static void UpdateFog(GridData grid, List<Unit> units)
    {
        // Step 1: downgrade old visibility
        for (int x = 0; x < grid.width; x++)
            for (int y = 0; y < grid.height; y++)
                if (grid.cells[x, y].fogState == FogState.Visible)
                    grid.cells[x, y].fogState = FogState.Explored;

        // Step 2: reveal from units
        foreach (var u in units)
        {
            for (int dx = -u.visionRange; dx <= u.visionRange; dx++)
            {
                for (int dy = -u.visionRange; dy <= u.visionRange; dy++)
                {
                    int nx = u.x + dx;
                    int ny = u.y + dy;

                    if (!grid.Inside(nx, ny)) continue;

                    int dist = Mathf.Abs(dx) + Mathf.Abs(dy);
                    if (dist > u.visionRange) continue;

                    grid.cells[nx, ny].fogState = FogState.Visible;
                }
            }
        }
    }


    /*void Awake()
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
    }*/
}
