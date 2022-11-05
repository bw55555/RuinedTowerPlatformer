using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class TerrainObject
{
    protected Vector2 gridPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPos(Vector2 gridPos)
    {
        this.gridPos = gridPos;
    }

    abstract public void instantiate();
}
