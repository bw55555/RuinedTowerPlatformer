using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine("Leave");
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine("Leave");
        }
    }

    IEnumerator Leave()
    {
        yield return new WaitForSeconds(1);
        MainController.Instance.nextLevel();
    }

}