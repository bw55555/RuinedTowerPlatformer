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


        generatePlatforms(top, bottom);

        generateVines(top, bottom);
    }

    void generatePlatforms(int top, int bottom)
    {
        int currLine = top;
        int randomIncr;
        while (currLine < bottom)
        {
            createPlatformOnLine(currLine);
            randomIncr = System.Math.Max(0, Random.Range(0, 4) - Random.Range(0, 1));
            currLine += randomIncr;
        }
    }

    void generateVines(int top, int bottom)
    {
        int currLine = top;
        int randomIncr;
        while (currLine < bottom)
        {
            createVineOnLine(currLine);
            randomIncr = Random.Range(0, 5);
            currLine += randomIncr;
        }
    }

    void createPlatformOnLine(int line)
    {
        int gridSize = Random.Range(3, 8);
        int gridStart = Random.Range(0, width - gridSize);
        //Debug.Log(line + " " + gridSize + " " + gridStart + " " + grid[line].Count);
        for (int i = gridStart; i < gridStart + gridSize; i++)
        {
            if (line >= 2 && grid[line - 2][i] != null && grid[line-2][i].getType() == TerrainType.Platform)
            {
                grid[line-1][i] = new Platform(new Vector2(line - 1, i));
            }
            grid[line][i] = new Platform(new Vector2(line, i));
        }
    }

    void createVineOnLine(int line)
    {
        if (line < 10) { return; }
        int col = Random.Range(0, width);
        int vineSize = Random.Range(3, 6);
        for (int i = line - vineSize + 1; i <= line; i++)
        {
            grid[i][col] = new Vine(new Vector2(i, col));
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
