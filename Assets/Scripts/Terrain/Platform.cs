using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : TerrainObject
{

    public Platform(Vector2 gridPos)
    {
        this.gridPos = gridPos;
    }

    override
    public void instantiate()
    {
        TerrainAssets.Instance.createPlatform(gridPos.y, gridPos.x);
    }
}
