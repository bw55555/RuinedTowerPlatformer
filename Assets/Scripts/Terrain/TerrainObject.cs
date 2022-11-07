using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TerrainType
{
    Platform,
    Vine,
    Door,
    NextLevelDoor,
    Wall,
    Torch,
    EnemySpawnLoc,
    ShopSpawnLoc,
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

    public Vector2Int getPos()
    {
        return gridPos;
    }

    public void setOverwritable(bool overwritable)
    {
        this.overwritable = overwritable;
    }

    public bool getOverwritable()
    {
        return this.overwritable;
    }

    virtual public void instantiate(GameObject parent)
    {
        TerrainAssets.Instance.createObject(getType(), parent, gridPos.y, -gridPos.x);
    }

    abstract public TerrainType getType();
}
