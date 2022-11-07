using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeStation : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform player;
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
            MainController.Instance.nextLevel();
        }
    }

    
}
