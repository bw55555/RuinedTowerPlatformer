using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : TerrainObject
{

    public Door(Vector2Int gridPos)
    {
        this.gridPos = gridPos;
    }


    override
    public TerrainType getType()
    {
        return TerrainType.Door;
    }
}
