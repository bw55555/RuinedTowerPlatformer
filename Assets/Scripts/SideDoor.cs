using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDoor : MonoBehaviour
{

    private Transform player;
    private GameObject roomGenerationPrefab;
    private RoomGenerate roomGeneration;
    private List<GameObject> roomList;

    public GameObject monsterRoomPrefab;
    public GameObject healthRoomPrefab;
    public GameObject chestRoomPrefab;
    public GameObject ropeRoomPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] playerarr;
        playerarr = GameObject.FindGameObjectsWithTag("Player");
        player = playerarr[0].transform;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag.Equals("Player") && Input.GetKeyDown(KeyCode.E))
        {
            int roomNum = Random.Range(0, 3);
            if (roomNum == 0)
            {
                roomGenerationPrefab = chestRoomPrefab;
            }
            else if (roomNum == 1)
            {
                roomGenerationPrefab = ropeRoomPrefab;
            }
            else if (roomNum == 2)
            {
                roomGenerationPrefab = monsterRoomPrefab;
            }
            else if (roomNum == 3)
            {
                roomGenerationPrefab = healthRoomPrefab;
            }
            
            
            roomGeneration = Instantiate(roomGenerationPrefab, new Vector3(player.position.x - 100, player.position.y, player.position.z), player.rotation).GetComponent<RoomGenerate>();
            player.position = new Vector3(player.position.x - 100, player.position.y, player.position.z);
        }
    }
}
