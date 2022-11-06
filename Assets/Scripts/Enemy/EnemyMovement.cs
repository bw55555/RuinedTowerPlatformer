using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public bool facingRight = true;
    private Transform player;
    public float moveSpeed = 1f;
    private float aggroTime = 0f;
    //public int enemyType;
    public float m_aggroSecs_outOfRange = 5;
    public float aggroRange = 20f;

    private float flipCd = 0f;

    private float stunTime = 0f;

    private Animator anim;

    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .06f; // Radius of the overlap circle to determine if grounded

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameObject[] playerarr;
        if (player == null)
        {
            playerarr = GameObject.FindGameObjectsWithTag("Player");
            player = playerarr[0].transform;
        }
        
        //healthBar.SetCurrent(currentHealth);
        //healthBar.SetMax(maxHealth);
    }

    public void Stun(float stunLength)
    {
        stunTime = stunLength;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        //animator to set direction here

    }

    void Flip()
    {
        Flip(false);
    }
    void Flip(bool overrideCd)
    {
        if (flipCd > 0 || !overrideCd) { return; }
        flipCd = 2f;
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void FixedUpdate()
    {
        stunTime -= Time.deltaTime;
        flipCd -= Time.deltaTime;
        aggroTime -= Time.deltaTime;
        if (Vector2.SqrMagnitude(player.transform.position - transform.position) < aggroRange)
        {
            Debug.Log(Vector2.SqrMagnitude(player.transform.position - transform.position));
            aggroTime = m_aggroSecs_outOfRange;
        }
        if (stunTime < 0)
        {
            if (aggroTime > 0)
            {
                if (Mathf.Sign(player.transform.position.x - transform.position.x) * (facingRight ? 1 : -1) < 0)
                {
                    Flip(true);
                }
                MoveCharacter(Mathf.Sign(player.transform.position.x - transform.position.x));
            }
            else
            {
                Patrol();
            }
        }

        if (rb.velocity.x * (facingRight ? 1 : -1) < 0)
        {
            Debug.Log("unflipped: " + rb.velocity.x + " " + facingRight);
            Flip(true);
            Debug.Log("flipped: " + rb.velocity.x + " " + facingRight);
        }
    }

    public void Patrol()
    {
        if (!checkForGroundAhead())
        {
            Flip();
        }
        MoveCharacter(facingRight ? 1 : -1);
    }

    public void MoveCharacter(float xDir)
    {
        if (checkForGroundAhead())
        {
            rb.velocity = new Vector2(xDir * moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    public bool checkForGroundAhead()
    {
        bool grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
            }
        }
        return grounded;
    }
}
