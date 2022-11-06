using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenStore : MonoBehaviour
{
    public Image prompt;
    public GameObject shop;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            prompt.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                shop.SetActive(true);
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            prompt.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                shop.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            prompt.enabled = false;
        }
    }
}
