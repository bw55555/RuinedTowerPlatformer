using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayerDown : MonoBehaviour
{
    // Start is called before the first frame update
    public float m_scrollUp = 5;
    public GameObject player;
    private float trackedValue;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        trackedValue = Mathf.Min(player.transform.position.y, trackedValue);

        transform.position = new Vector3(player.transform.position.x, 
            Mathf.Min(player.transform.position.y, trackedValue + m_scrollUp), player.transform.position.z);
    }
}
