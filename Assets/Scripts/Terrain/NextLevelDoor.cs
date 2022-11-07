using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelDoor : TerrainObject
{

    public NextLevelDoor(Vector2Int gridPos)
    {
        this.gridPos = gridPos;
    }


    override
    public TerrainType getType()
    {
        return TerrainType.NextLevelDoor;
    }
}
