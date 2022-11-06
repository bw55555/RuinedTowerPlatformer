﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayerDown : MonoBehaviour
{
    // Start is called before the first frame update
    public float m_scrollUp = 5;
    public GameObject player;
    private float trackedValue;
    public int bottomClamp = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        trackedValue = Mathf.Clamp(player.transform.position.y, - 1 * MainController.Instance.endHeight() + bottomClamp, trackedValue);

        transform.position = new Vector3(player.transform.position.x, 
            Mathf.Clamp(player.transform.position.y, -1 * MainController.Instance.endHeight() + bottomClamp, trackedValue + m_scrollUp), player.transform.position.z);
    }
}
