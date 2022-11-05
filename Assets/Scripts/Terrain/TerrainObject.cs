using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TerrainType
{
    Platform,
    Vine,
    Empty
}

abstract public class TerrainObject
{
    protected Vector2 gridPos;

    public void setPos(Vector2 gridPos)
    {
        this.gridPos = gridPos;
    }

    public void instantiate()
    {
        TerrainAssets.Instance.createObject(getType(), gridPos.y, gridPos.x);
    }

    abstract public TerrainType getType();
}
