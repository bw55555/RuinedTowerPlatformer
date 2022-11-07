using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSpawnLoc : TerrainObject
{

    public ShopSpawnLoc(Vector2Int gridPos)
    {
        this.gridPos = gridPos;
    }


    override
    public TerrainType getType()
    {
        return TerrainType.ShopSpawnLoc;
    }
}
