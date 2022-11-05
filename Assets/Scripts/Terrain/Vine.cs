using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : TerrainObject
{

    public Vine(Vector2Int gridPos)
    {
        this.gridPos = gridPos;
    }

    override
    public TerrainType getType()
    {
        return TerrainType.Vine;
    }
}
