using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public static MainController Instance;

    public GameObject terrainGenerationPrefab;
    private TerrainGeneration terrainGeneration;
    public GameObject BigCamera;
    public Transform lastPosition;

    public int level = 0;
    private float scaling;

    private Transform player;

    public ProgressBar distanceBar;

    public UnityEvent toNextLevel;

    public GameObject shop_shop;
    public Image shop_prompt;
    public int Level { get => level; set => level = value; }

    private void Awake()
    {
        Instance = this;
        GameObject[] playerarr;
        if (player == null)
        {
            playerarr = GameObject.FindGameObjectsWithTag("Player");
            if (playerarr.Length > 0)
            {
                player = playerarr[0].transform;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        nextLevel();
    }

    public void nextLevel()
    {
        if (terrainGeneration != null)
        {
            Destroy(terrainGeneration.gameObject);
        }
        level += 1;
        terrainGeneration = Instantiate(terrainGenerationPrefab).GetComponent<TerrainGeneration>();
        terrainGeneration.height = 50 + level * 50 + Mathf.Clamp(level -1, 0, 5) * 50;
        terrainGeneration.createTerrain();
        distanceBar.setMaxValue(endHeight());
        player.position = new Vector3(terrainGeneration.width / 2, 0, 0);
        player.GetComponent<PlayerInfo>().setIframes(1f);
        SoundManager.Instance.playSound(SoundManager.Instance.next_level);
        toNextLevel.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        distanceBar.setValue(Mathf.Clamp(-player.position.y, 0, endHeight()));
        
    }

    public int endHeight()
    {
        return terrainGeneration.getGridHeight();
    }

    public bool isInSideRoom()
    {
        return player.position.x < -terrainGeneration.width / 2 - 5;
    }
}
