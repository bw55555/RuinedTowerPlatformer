using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public bool facingRight = true;

    public float moveSpeed = 1f;
    private float aggroTime = 0f;
    //public int enemyType;
    public float m_aggroSecs_outOfRange = 5;
    public float aggroRange = 20f;

    private float flipCd = 0f;

    private float stunTime = 0f;

    private Animator anim;

    private Enemy enemy;
    private Transform player;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (player == null)
        {
            GameObject[] playerarr;
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
        if (rb.velocity.x * (facingRight ? 1 : -1) < 0)
        {
            Flip(true);
        }
        //anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

    }

    void Flip()
    {
        Flip(false);
    }
    void Flip(bool overrideCd)
    {
        if (flipCd > 0 && !overrideCd) { return; }
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
                MoveCharacter((player.transform.position - transform.position).normalized);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }


    }

    public void Patrol()
    {
        MoveCharacter(new Vector2(facingRight ? 1 : -1, 0));
    }

    public void MoveCharacter(Vector2 dir)
    {
         rb.velocity = new Vector2(dir.x * moveSpeed, dir.y * moveSpeed);
    }
}
