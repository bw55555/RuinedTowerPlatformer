using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerate : MonoBehaviour
{
    public int width = 56;
    public int height;

    public int enemy_density = 5;
    public int enemy_spacing = 3;
    public int flying_enemy_density = 15;
    public int knight_enemy_density = 10;
    public int demon_enemy_chance = 5;
    public int knight_enemy_chance = 10;

    private List<List<TerrainObject>> grid = new List<List<TerrainObject>>();

    public int getGridHeight()
    {
        return grid.Count;
    }

    // Start is called before the first frame update
    void Start()
    {
        //createTerrain();
    }

    public void createTerrain()
    {
        instantiate();
    }

    bool equals(TerrainType x, TerrainType y)
    {
        return x == y || x == TerrainType.Any || y == TerrainType.Any;
    }

    void instantiate()
    {
        for (int i = 0; i < grid.Count; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[i][j] != null)
                {
                    //grid[i][j].setPos(i * 10, j * 10);
                    grid[i][j].instantiate();
                }
            }
        }
    }
}
