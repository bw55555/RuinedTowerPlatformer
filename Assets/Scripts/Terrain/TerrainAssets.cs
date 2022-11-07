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
    public TileBase backgroundTile;

    public GameObject torch;
    public GameObject door;
    public GameObject nextLevelDoor;
    public GameObject shop;
    public static TerrainAssets Instance;

    

    private void Awake()
    {
        Instance = this;

    }

    public Tilemap platformTileMap;
    public Tilemap backgroundTileMap;
    public void createObject(TerrainType type, GameObject parent, int xpos, int ypos)
    {
        switch (type)
        {
            case TerrainType.Platform: platformTileMap.SetTile(new Vector3Int(xpos, ypos, 0), platform); return;
            case TerrainType.Wall: platformTileMap.SetTile(new Vector3Int(xpos, ypos, 0), wall); return;
            case TerrainType.Torch: Instantiate(torch, new Vector3Int(xpos, ypos, 0), Quaternion.identity, parent.transform); return;
            case TerrainType.Door: Instantiate(door, new Vector3Int(xpos, ypos, 1), Quaternion.identity, parent.transform); return;
            case TerrainType.NextLevelDoor: Instantiate(nextLevelDoor, new Vector3Int(xpos, ypos, 1), Quaternion.identity, parent.transform); return;
            case TerrainType.ShopSpawnLoc: 
                OpenStore s = Instantiate(shop, new Vector3Int(xpos, ypos, 1), Quaternion.identity, parent.transform).GetComponent<OpenStore>();
                s.shop = MainController.Instance.shop_shop;
                s.prompt = MainController.Instance.shop_prompt;
                MainController.Instance.shop_shop.GetComponent<PickSkills>().origin = s.gameObject;
                return;
        }

    }

    public void createBackground(Vector3Int[] tiles)
    {
        TileBase[] tilebase = new TileBase[tiles.Length];
        for (int i = 0;i<tiles.Length;i++)
        {
            tilebase[i] = backgroundTile;
        }

        backgroundTileMap.SetTiles(tiles, tilebase);
    }
}
