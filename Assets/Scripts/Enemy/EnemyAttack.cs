using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public float stunOnAttack;
    public float attackRange;
    public float minTimeBetweenAttacks;

    private Animator anim;
    private Transform player;

    private float attackCd;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        if (player == null)
        {
            GameObject[] playerarr;
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
        if (attackCd <= 0 && shouldAttack())
        {
            attackCd = minTimeBetweenAttacks;
            SoundManager.Instance.playSound(SoundManager.Instance.flyingknight_attack);
            anim.SetTrigger("Attack");
        }
    }

    bool shouldAttack()
    {
        if (Mathf.Abs(player.position.x - transform.position.x) < attackRange && Mathf.Abs(player.position.y - transform.position.y) < 2)
        {
            return true;
        }
        return false;
    }
}
