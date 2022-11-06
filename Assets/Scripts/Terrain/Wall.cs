using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : TerrainObject
{

    public Wall(Vector2Int gridPos)
    {
        this.gridPos = gridPos;
    }

    override
    public TerrainType getType()
    {
        return TerrainType.Wall;
    }
}
