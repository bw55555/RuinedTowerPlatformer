﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    public int width;
    public int height;

    

    public float platform_spacing = 1.6f;
    public int platform_minSize = 3;
    public int platform_maxSize = 8;
    public int vine_spacing = 10;
    public int vine_minSize = 3;
    public int vine_maxSize = 8;
    public int door_minspacing = 15;
    public int door_maxspacing = 25;
    public int door_findRange = 25;

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
                grid[grid.Count - 1].Add(new Empty());
            }
            
        }


        generatePlatforms(top, bottom);

        generateVines(top, bottom);

        generateDoors(top, bottom);
    }

    void generatePlatforms(int top, int bottom)
    {
        float currLine = top;
        float randomIncr;
        while (currLine < bottom)
        {
            createPlatformOnLine((int) currLine);
            randomIncr = Random.Range(0.0f, platform_spacing);
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
            randomIncr = Random.Range(0, vine_spacing);
            currLine += randomIncr;
        }
    }

    void generateDoors(int top, int bottom)
    {
        TerrainType[] block1 = { TerrainType.Empty };
        TerrainType[] block2 = { TerrainType.Empty };
        TerrainType[] block3 = { TerrainType.Empty };
        TerrainType[] block4 = { TerrainType.Platform };
        TerrainType[][] pattern = new TerrainType[][] {
            block1, block2, block3, block4
        };
        int currLine = top;
        while (currLine < bottom)
        {
            List<Vector2Int> eligibleLocations = findByPattern(currLine, 0, currLine + door_findRange, width, pattern);
            Debug.Log("Count:" + eligibleLocations.Count);
            if (eligibleLocations.Count > 0)
            {
                Vector2Int randomLoc = eligibleLocations[Random.Range(0, eligibleLocations.Count - 1)];
                Debug.Log("RandomLoc:" + randomLoc.x + " " + randomLoc.y);
                grid[randomLoc.x + 1][randomLoc.y] = new Air(randomLoc + new Vector2Int(1, 0), false);
                grid[randomLoc.x + 2][randomLoc.y] = new Door(randomLoc + new Vector2Int(2, 0));
                currLine = randomLoc.x;
            }
            
            currLine += Random.Range(door_minspacing, door_maxspacing);
        }
        
    }

    void createPlatformOnLine(int line)
    {
        int gridSize = Random.Range(platform_minSize, platform_maxSize);
        int gridStart = Random.Range(0, width - gridSize);
        //Debug.Log(line + " " + gridSize + " " + gridStart + " " + grid[line].Count);
        for (int i = gridStart; i < gridStart + gridSize; i++)
        {
            if (line >= 2 && grid[line - 2][i] != null && grid[line-2][i].getType() == TerrainType.Platform)
            {
                grid[line-1][i] = new Platform(new Vector2Int(line - 1, i));
            }
            grid[line][i] = new Platform(new Vector2Int(line, i));
        }
    }

    void createVineOnLine(int line)
    {
        if (line < vine_maxSize + 1) { return; }
        int col = Random.Range(0, width);
        int vineSize = Random.Range(vine_minSize, vine_maxSize);
        for (int i = line - vineSize + 1; i <= line; i++)
        {
            grid[i][col] = new Vine(new Vector2Int(i, col));
        }
    }

    List<Vector2Int> findByPattern(int top, int left, int bottom, int right, TerrainType[][] pattern)
    {
        List<Vector2Int> patternMatches = new List<Vector2Int>();
        for (int i = top; i < bottom; i++)
        {
            for (int j = left; j < right; j++)
            {
                if (matchPattern(i, j, pattern))
                {
                    Debug.Log(i + " " + j);
                    patternMatches.Add(new Vector2Int(i, j));
                }
            }
        }
        return patternMatches;

    }

    bool matchPattern(int x, int y, TerrainType[][] pattern)
    {
        //Debug.Log("Start: "+ x + " " + y);
        bool correctPattern = true;
        for (int i = 0; i < pattern.Length; i++)
        {
            for (int j = 0;j < pattern[i].Length;j++)
            {
                //Debug.Log("i+x: " + (i + x) + " j+y: " + (j + y) + " grid: " + grid[i + x][j + y].getType() + " pattern: " + pattern[i][j]);
                if (i + x >= grid.Count || j + y >= grid[i].Count)
                {
                    return false;
                } else
                {
                    correctPattern = equals(grid[i + x][j + y].getType(), pattern[i][j]);
                }
                if (!correctPattern) { return false; }
            }
        }
        return correctPattern;
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
