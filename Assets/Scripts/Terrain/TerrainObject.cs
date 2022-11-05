﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TerrainType
{
    Platform,
    Vine,
    Door,
    Air,
    Empty, 
    Any
}

abstract public class TerrainObject
{
    protected Vector2Int gridPos;
    protected bool overwritable = false;

    public void setPos(Vector2Int gridPos)
    {
        this.gridPos = gridPos;
    }

    public void setOverwritable(bool overwritable)
    {
        this.overwritable = overwritable;
    }

    public bool getOverwritable()
    {
        return this.overwritable;
    }

    public void instantiate()
    {
        TerrainAssets.Instance.createObject(getType(), gridPos.y, -gridPos.x);
    }

    abstract public TerrainType getType();
}
