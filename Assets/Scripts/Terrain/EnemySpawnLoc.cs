using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Slime = 0,
    Skeleton = 1,
    Knight = 2, 
    Demon = 3
}

public class EnemySpawnLoc : TerrainObject
{
    private EnemyType enemyType;

    public EnemySpawnLoc(Vector2Int gridPos, EnemyType etype)
    {
        this.gridPos = gridPos;
        enemyType = etype;
    }

    override
    public TerrainType getType()
    {
        return TerrainType.EnemySpawnLoc;
    }
    /*
    override
        public void instantiate()
    {

    }
    */
}
