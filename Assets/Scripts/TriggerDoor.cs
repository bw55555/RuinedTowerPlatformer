using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerDoor : MonoBehaviour
{
    public Animator anim;
    public Image blac;
    public int scene;


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
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => blac.color.a == 1);
        SceneManager.LoadScene(scene);
    }

}