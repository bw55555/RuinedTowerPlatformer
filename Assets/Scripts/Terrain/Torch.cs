using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : TerrainObject
{

    public Torch(Vector2Int gridPos)
    {
        this.gridPos = gridPos;
    }


    override
    public TerrainType getType()
    {
        return TerrainType.Torch;
    }
}
