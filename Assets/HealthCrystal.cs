using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCrystal : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform player;
    private GameObject[] playerarr;
    // Start is called before the first frame update
    void Start()
    {
        
        playerarr = GameObject.FindGameObjectsWithTag("Player");
        player = playerarr[0].transform;
    }

    void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Player") && Input.GetKeyDown(KeyCode.E))
        {
            PlayerInfo play = playerarr[0].GetComponent<PlayerInfo>();
            play.healthBar.setValue(play.MaxHealth);
        }
    }

    
}
