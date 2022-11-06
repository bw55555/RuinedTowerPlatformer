using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private Rigidbody2D rb;
    public int currentHealth;
    public float thrust;
    private Vector2 movement;
    
    public bool facingRight = true;
    private Transform player;
    public float moveSpeed = 1f;
    private float aggroTime = 0f;
    public int enemyType;
    public int minDistanceToPlayer;

    public float m_aggroSecs_outOfRange = 5;
    public float aggroRange = 5f;

    private float flipCd = 0f;

    private float stunTime = 0f;
    // Start is called before the first frame update

    private Animator anim;

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
        currentHealth = maxHealth;
        //healthBar.SetCurrent(currentHealth);
        //healthBar.SetMax(maxHealth);
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
        if (flipCd > 0) { return; }
        flipCd = 2f;
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void Stun(float stunLength)
    {
        stunTime = stunLength;
    }
    // Update is called once per frame
    public void TakeDamage(int damage, bool wind = false, bool water = false)
    {
        aggroTime = m_aggroSecs_outOfRange;
        Debug.Log("took damage", gameObject);
        currentHealth -= damage;
        //healthBar.SetCurrent(currentHealth, transform.localScale.x < 0);
        //healthBar.ToggleActive(true);
        Vector2 difference = transform.position - player.position;
        difference = difference.normalized * thrust;
        Stun(0.3f);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void FixedUpdate()
    {
        stunTime -= Time.deltaTime;
        flipCd -= Time.deltaTime;
        aggroTime -= Time.deltaTime;
        if (Vector2.Distance(player.transform.position, transform.position) < aggroRange)
        {
            aggroTime = m_aggroSecs_outOfRange;
        }
        if (stunTime < 0) { MoveCharacter(movement); }
        if (aggroTime > 0)
        {
            MoveCharacter(new Vector2(player.transform.position.x - transform.position.x, 0).normalized);
        } else
        {
            Patrol();
        }
    }

    public void Patrol()
    {
        if (!checkForGroundAhead())
        {
            Flip();
        }
        MoveCharacter(new Vector2(facingRight ? 1 : 0, 0));
    }

    public void MoveCharacter(Vector2 direction)
    {
        if (checkForGroundAhead())
        {
            rb.velocity = direction * moveSpeed;
        }
    }

    public bool checkForGroundAhead()
    {
        return true;
    }
    void Die()
    {
        Debug.Log("You killed an enemy");
        //healthBar.ToggleActive(false);
        //anim.SetTrigger("Dead");
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 0.5f);
    }
}
