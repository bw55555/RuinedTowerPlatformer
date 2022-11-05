using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainAssets : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject platform;
    public GameObject vine;
    public static TerrainAssets Instance;

    private void Awake()
    {
        Instance = this;

    }


    public GameObject createObject(TerrainType type, float xpos, float ypos)
    {
        switch (type)
        {
            case TerrainType.Platform: return Instantiate(platform, new Vector3(xpos, ypos, 0), Quaternion.identity);
            case TerrainType.Vine: return Instantiate(vine, new Vector3(xpos, ypos, 0), Quaternion.identity);
            default: return null;
        }
        
    }
}
