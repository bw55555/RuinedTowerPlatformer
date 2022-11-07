using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
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
            Debug.Log("DANANANNANANANa");
        }
    }

    
}
