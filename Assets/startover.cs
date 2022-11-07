using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startover : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start_Over()
    {
        SceneManager.LoadScene("Intro");
    }
}
