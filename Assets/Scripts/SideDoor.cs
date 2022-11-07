using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDoor : MonoBehaviour
{

    private Transform player;
    private GameObject roomGenerationPrefab;
    private RoomGenerate roomGeneration;
    private List<GameObject> roomList;
    public GameObject torch;
    public bool Checked;

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
        Checked = false;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        checkEnterRoom(collision);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        checkEnterRoom(collision);
    }

    void checkEnterRoom(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !Checked && Input.GetKeyDown(KeyCode.E))
        {
            int roomNum = Random.Range(2, 4);
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

            MainController.Instance.lastPosition = gameObject.transform;
            int xPos = Mathf.RoundToInt(player.position.x - 100);
            int yPos = Mathf.RoundToInt(player.position.y);
            roomGeneration = Instantiate(roomGenerationPrefab, new Vector3(xPos, yPos + 4, player.position.z + 1), player.rotation).GetComponent<RoomGenerate>();
            player.position = new Vector3(player.position.x - 92, player.position.y, player.position.z);
            Checked = true;
            Instantiate(torch, new Vector3Int(xPos - 5, yPos, 0), Quaternion.identity);
            Instantiate(torch, new Vector3Int(xPos + 5, yPos, 0), Quaternion.identity);
        }
    }
}
