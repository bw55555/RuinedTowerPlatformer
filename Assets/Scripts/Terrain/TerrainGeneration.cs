using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    public int width;
    public int height;
    private List<List<TerrainObject>> grid = new List<List<TerrainObject>>();

    // Start is called before the first frame update
    void Start()
    {
        generate(0, height);
        instantiate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generate(int top, int bottom)
    {
        while (grid.Count < bottom)
        {
            grid.Add(new List<TerrainObject>());
            while (grid[grid.Count - 1].Count < width)
            {
                grid[grid.Count - 1].Add(null);
            }
            
        }

        int currLine = top;
        int randomIncr;
        while (currLine < bottom)
        {
            createPlatformOnLine(currLine);
            randomIncr = Random.Range(0, 3);
            currLine += randomIncr;
            
        }
    }

    void createPlatformOnLine(int line)
    {
        int gridSize = Random.Range(3, 6);
        int gridStart = Random.Range(0, width - gridSize);
        Debug.Log(line + " " + gridSize + " " + gridStart + " " + grid[line].Count);
        for (int i = gridStart; i < gridStart + gridSize; i++)
        {
            grid[line][i] = new Platform(new Vector2(line, i));
        }
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
