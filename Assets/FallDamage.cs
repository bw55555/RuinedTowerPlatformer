using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{
    public Rigidbody2D rb2D;
    private PlayerInfo playerInfo;
    private float speedBeforeLanding;
    private void Awake()
    {
        playerInfo = GetComponent<PlayerInfo>();
    }
   
    void Start() => rb2D = GetComponent<Rigidbody2D>();
    private float GetVerticalSpeed() => rb2D.velocity.y;

    

    private void takeSmallFallDamage() 
    {
        
    }
   
    // Update is called once per frame
    private void Update()
    {
        if (GetVerticalSpeed() < 0)
        {
        speedBeforeLanding = GetVerticalSpeed();
        }

        if (speedBeforeLanding < -15 && GetVerticalSpeed() == 0) {
            
        }
        /*and the feet is on ground
        if (GetVerticalSpeed() == 0) 
        {
            if (speedBeforeLanding > 0.59){
                Destroy(gameObject, 0.1f);
            }
        }
        */
    }
}
