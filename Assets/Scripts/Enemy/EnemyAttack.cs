using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public float stunOnAttack;
    public float attackRange = 1f;
    public float minTimeBetweenAttacks;
    public Transform hitbox;
    public LayerMask playerLayer;
    private GameObject[] playerarr;

    private Animator anim;
    private Transform player;

    private float attackCd;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        if (player == null)
        {
            
            playerarr = GameObject.FindGameObjectsWithTag("Player");
            player = playerarr[0].transform;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        attackCd -= Time.deltaTime;
        Enemy enemy = gameObject.GetComponent<Enemy>();
        if (attackCd <= 0 && shouldAttack())
        {
            attackCd = minTimeBetweenAttacks;
            SoundManager.Instance.playSound(SoundManager.Instance.flyingknight_attack);
            anim.SetTrigger("Attack");

            Collider2D[] hit = Physics2D.OverlapCircleAll(hitbox.position, attackRange, playerLayer);

            foreach (Collider2D coll in hit)
            {
                if (coll.gameObject.tag.Equals("Player"))
                {
                    PlayerInfo play = playerarr[0].GetComponent<PlayerInfo>();
                    play.takeDamage(enemy.Attack);
                }
            }
        }
    }

    bool shouldAttack()
    {
        if (Mathf.Abs(player.position.x - transform.position.x) < attackRange + 3 && Mathf.Abs(player.position.y - transform.position.y) < 2)
        {
            return true;
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        if (hitbox == null)
            return;

        Gizmos.DrawWireSphere(hitbox.position, attackRange);
    }
}
