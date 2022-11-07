using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainAssets : MonoBehaviour
{
    // Start is called before the first frame update
    public TileBase platform;
    public TileBase vine;
    public TileBase wall;

    public GameObject torch;
    public GameObject door;
    public static TerrainAssets Instance;

    public GameObject skeleton;
    public GameObject slime;
    public GameObject knight;
    public GameObject demon;

    private void Awake()
    {
        Instance = this;

    }

    public Tilemap platformTileMap;
    public Tilemap vineTileMap;
    public void createObject(TerrainType type, int xpos, int ypos)
    {
        switch (type)
        {
            case TerrainType.Platform: platformTileMap.SetTile(new Vector3Int(xpos, ypos, 0), platform); return;
            case TerrainType.Wall: platformTileMap.SetTile(new Vector3Int(xpos, ypos, 0), wall); return;
            case TerrainType.EnemySpawnLoc: vineTileMap.SetTile(new Vector3Int(xpos, ypos, 0), vine); return;
            case TerrainType.Torch: Instantiate(torch, new Vector3Int(xpos, ypos, 0), Quaternion.identity); return;
            case TerrainType.Door: Instantiate(door, new Vector3Int(xpos, ypos, 0), Quaternion.identity); return;
        }

    }

    public void createEnemy(EnemyType type, float xpos, float ypos)
    {
        switch (type)
        {
            case EnemyType.Slime: Instantiate(slime, new Vector3(xpos + 0.5f, ypos + 0.5f, 0), Quaternion.identity); return;
            case EnemyType.Skeleton: Instantiate(skeleton, new Vector3(xpos + 0.5f, ypos + 1.5f, 0), Quaternion.identity); return;
            case EnemyType.Knight: Instantiate(knight, new Vector3(xpos + 1f, ypos, 0), Quaternion.identity); return;
            case EnemyType.Demon: Instantiate(demon, new Vector3(xpos + 1f, ypos, 0), Quaternion.identity); return;
        }
    }
}
