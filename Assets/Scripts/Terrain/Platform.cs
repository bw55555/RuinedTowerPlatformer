using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : TerrainObject
{

    public Platform(Vector2Int gridPos)
    {
        this.gridPos = gridPos;
    }


    override
    public TerrainType getType()
    {
        return TerrainType.Platform;
    }
}
