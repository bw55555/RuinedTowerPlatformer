using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitRoom : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] playerarr;
        playerarr = GameObject.FindGameObjectsWithTag("Player");
        player = playerarr[0].transform;
    }

    void OnTriggerStay2D(Collider2D collision)
    {

        checkLeaveRoom(collision);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        checkLeaveRoom(collision);
    }

    void checkLeaveRoom(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && Input.GetKeyDown(KeyCode.E))
        {
            float xPos = MainController.Instance.lastPosition.position.x;
            float yPos = MainController.Instance.lastPosition.position.y;
            player.position = new Vector3(xPos, yPos, player.position.z);
        }
    }

}
