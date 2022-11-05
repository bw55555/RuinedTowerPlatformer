using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOSS : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    Transform castPoint;

    [SerializeField]
    float agroRange;
    
    [SerializeField]
    float moveSpeed;

    Rigidbody2D rb;
    EnemyAI path;
    
    public bool isFacingLeft;

    public Transform enemyGFX;
    public float time;
    public bool isAgro = false;
    private bool isSearching;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        path = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        isFacingLeft = path.isFacingLeft;
        if(CanSeePlayer(agroRange))
        {
            // add delay and animation here
            if(!isAgro)
            {
                Invoke("ApeMode", 2);
            }
        }
        else
        {
            if(isAgro)
            {
                if(!isSearching)
                {
                    isSearching = true;
                    Debug.Log("Searching");
                    Invoke("StopChasePlayer", time);
                }
            }
        }

        if(isAgro)
        {
            ChasePlayer();
        }

    }

    bool CanSeePlayer(float distance)
    {
        bool value = false;
        var castDist = distance;

        if(isFacingLeft)
        {
            castDist = -distance;
        }

        Vector2 endPos = castPoint.position + Vector3.right * castDist;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));

        if(hit.collider != null)
        {
            if(hit.collider.gameObject.CompareTag("Player"))
            {
                value = true;
            }
            else
            {
                value = false;
            }

            Debug.DrawRay(castPoint.position, hit.point, Color.yellow);
        }
        else
        {
            Debug.DrawRay(castPoint.position, endPos, Color.blue);
        }

        return value;
    }

    void ChasePlayer()
    {
        if(enemyGFX.position.x < player.position.x)
        {
            rb.velocity = new Vector2(moveSpeed, 0);
            enemyGFX.localScale = new Vector2(1,1);
            isFacingLeft = false;
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, 0);
            enemyGFX.localScale = new Vector2(-1, 1);
            isFacingLeft = true;
        }
    }

    void StopChasePlayer()
    {
        isSearching = false;
        isAgro = false;
    }

    void ApeMode()
    {
        isAgro = true;
    }
}
