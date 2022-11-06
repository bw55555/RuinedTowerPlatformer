using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empty : TerrainObject
{

    public Empty()
    {
    }

    override
    public TerrainType getType()
    {
        return TerrainType.Empty;
    }
}
