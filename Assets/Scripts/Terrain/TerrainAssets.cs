using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainAssets : MonoBehaviour
{
    // Start is called before the first frame update
    public TileBase platform;
    public TileBase vine;
    public TileBase door;
    public TileBase wall;
    public static TerrainAssets Instance;

    private void Awake()
    {
        Instance = this;

    }

    public Tilemap platformTileMap;
    public Tilemap vineTileMap;
    public Tilemap doorTileMap;
    public void createObject(TerrainType type, int xpos, int ypos)
    {
        switch (type)
        {
            case TerrainType.Platform: platformTileMap.SetTile(new Vector3Int(xpos, ypos, 0), platform); return;
            case TerrainType.Wall: platformTileMap.SetTile(new Vector3Int(xpos, ypos, 0), wall); return;
            case TerrainType.Vine: vineTileMap.SetTile(new Vector3Int(xpos, ypos, 0), vine); return;
            case TerrainType.Door: doorTileMap.SetTile(new Vector3Int(xpos, ypos, 0), door); return;
        }

    }


}
