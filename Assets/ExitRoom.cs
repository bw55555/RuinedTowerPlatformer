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

        if (collision.gameObject.tag.Equals("Player") && Input.GetKeyDown(KeyCode.E))
        {
            player.position = new Vector3(player.position.x + 92, player.position.y + 4, player.position.z);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Player") && Input.GetKeyDown(KeyCode.E))
        {
            player.position = new Vector3(player.position.x + 92, player.position.y + 4, player.position.z);
        }
    }
}
