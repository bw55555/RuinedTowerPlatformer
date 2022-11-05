using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainAssets : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject platform;
    public GameObject vines;
    public static TerrainAssets Instance;

    private void Awake()
    {
        Instance = this;

    }


    public GameObject createPlatform(float xpos, float ypos)
    {
        return Instantiate(platform, new Vector3(xpos, ypos, 0), Quaternion.identity);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
