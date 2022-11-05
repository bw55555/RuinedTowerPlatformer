using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empty : TerrainObject
{

    public Empty(Vector2 gridPos)
    {
        this.gridPos = gridPos;
    }

    override
    public TerrainType getType()
    {
        return TerrainType.Empty;
    }
}
