using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    public int width = 56;
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
    public int torch_spacing = 7;
    public int enemy_density = 5;
    public int enemy_spacing = 3;

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
        generateSectionStart();
        generate(11, height);

        instantiate();
    }

    void generateSectionStart()
    {
        for (int i = 0; i < 10; i++)
        {
            grid.Add(new List<TerrainObject>());
            for (int j = 0; j < width; j++)
            {
                grid[i].Add(new Empty());
            }

        }
        int middle = width / 2;
        for (int i = 0;i < 4;i++)
        {
            grid[5][middle - 2 + i] = new Platform(new Vector2Int(5, middle - 2 + i));
        }
        for (int i = 0; i < 5; i++)
        {
            grid[8][middle - 2 - i] = new Platform(new Vector2Int(8, middle - 2 - i));
        }
        for (int i = 0; i < 5; i++)
        {
            grid[8][middle + 1 + i] = new Platform(new Vector2Int(8, middle + 1 + i));
        }
    }

    void generateSectionEnd()
    {
        int currHeight = grid.Count;
        for (int i = 0;i<5;i++)
        {
            grid.Add(new List<TerrainObject>());
            for (int j = 0; j < width; j++)
            {
                grid[grid.Count - 1].Add(new Empty());
            }

        }

        for (int i = 0; i < 5; i++)
        {
            grid.Add(new List<TerrainObject>());
            for (int j = 0; j < width; j++)
            {
                grid[grid.Count - 1].Add(new Wall(new Vector2Int(grid.Count - 1, j)));
            }
        }

        generateWalls(currHeight, grid.Count);
    }

    void generateWalls(int top, int bottom)
    {
        bottom = Mathf.Min(bottom, grid.Count);
        for (int i = top;i<bottom;i++)
        {
            grid[i][0] = new Wall(new Vector2Int(i, 0));
            grid[i][1] = new Wall(new Vector2Int(i, 1));
            grid[i][2] = new Wall(new Vector2Int(i, 2));
            grid[i][width - 3] = new Wall(new Vector2Int(i, width - 3));
            grid[i][width - 2] = new Wall(new Vector2Int(i, width - 2));
            grid[i][width - 1] = new Wall(new Vector2Int(i, width - 1));
        }
    }

    void generateTorches(int top, int bottom)
    {
        TerrainType[] block1 = { TerrainType.Empty };
        TerrainType[][] pattern = { block1 };
        int torch_chance = 5;
        for (int x = top; x < bottom; x++)
        {
            for (int y = 0; y < width; y++)
            {
                if (grid[x][y].getType() == TerrainType.Empty && isSpacedOut(x, y, torch_spacing, TerrainType.Torch))
                {
                    if (Random.Range(1, torch_chance) == 1)
                    {
                        grid[x][y] = new Torch(new Vector2Int(x, y));
                    }
                }
            }
        }
    }

    void generateMeleeEnemies(int top, int bottom)
    {
        TerrainType[] block1 = { TerrainType.Any, TerrainType.Empty, TerrainType.Any };
        TerrainType[] block2 = { TerrainType.Any, TerrainType.Empty, TerrainType.Any };
        TerrainType[] block3 = { TerrainType.Any, TerrainType.Empty, TerrainType.Any };
        TerrainType[] block4 = { TerrainType.Platform, TerrainType.Platform, TerrainType.Platform};
        TerrainType[][] pattern = new TerrainType[][] {
            block1, block2, block3, block4
        };
        int currLine = top;
        while (currLine < bottom)
        {
            List<Vector2Int> eligibleLocations = findByPattern(currLine, 0, currLine + 10, width, pattern);
            Debug.Log("Count:" + eligibleLocations.Count);
            if (eligibleLocations.Count > 0)
            {
                Vector2Int randomLoc = eligibleLocations[Random.Range(0, eligibleLocations.Count)];
                if (isSpacedOut(randomLoc.x, randomLoc.y, enemy_spacing, TerrainType.EnemySpawnLoc))
                {
                    EnemyType enemyType = (EnemyType) Random.Range(0, 2);
                    grid[randomLoc.x + 2][randomLoc.y + 1] = new EnemySpawnLoc(randomLoc + new Vector2Int(2, 1), enemyType);
                }
            }

            currLine += Random.Range(0, enemy_density);
        }

    }

    bool isSpacedOut(int x, int y, int spacing, TerrainType t)
    {
        
        for (int i = x - spacing; i <= x + spacing; i++)
        {
            for (int j = y - spacing; j <= y + spacing; j++)
            {
                if (i < 0 || i >= grid.Count || j < 0 || j >= width) { continue; }
                    
                if(grid[i][j].getType() == t)
                {
                    return false;
                }
            }
        }
        return true;
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
        
        generateWalls(0, grid.Count);

        generateMeleeEnemies(top, bottom);

        generateDoors(top, bottom);

        generateTorches(top, bottom);

        generateSectionEnd();
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
            //Debug.Log("Count:" + eligibleLocations.Count);
            if (eligibleLocations.Count > 0)
            {
                Vector2Int randomLoc = eligibleLocations[Random.Range(0, eligibleLocations.Count)];
                //Debug.Log("RandomLoc:" + randomLoc.x + " " + randomLoc.y);
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
        if (line < 10 + vine_maxSize + 1) { return; }
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
                    //Debug.Log(i + " " + j);
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
