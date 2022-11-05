using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : TerrainObject
{

    public Air(Vector2Int gridPos)
    {
        this.gridPos = gridPos;
    }

    public Air(Vector2Int gridPos, bool overwritable)
    {
        this.gridPos = gridPos;
        this.overwritable = overwritable;
    }

    override
    public TerrainType getType()
    {
        return TerrainType.Air;
    }
}
