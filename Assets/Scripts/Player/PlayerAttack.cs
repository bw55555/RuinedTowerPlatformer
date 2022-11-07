using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    public Transform hitbox;
    public float attackRange = 1f;
    public LayerMask enemyLayers;
    public Animator animator;
    private PlayerInfo playerInfo;

    private void Awake()
    {
        playerInfo = GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Skill s = SkillContainer.Instance.getSkill(SkillType.Attack);
            if (s.isReady())
            {
                
               
                s.useSkill();
                Attack();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.collider.gameObject.GetComponent<Enemy>();
        Debug.Log("Collision Detected", collision.otherCollider);
        if (enemy != null)
        {
            Debug.Log("something is happening");
            playerInfo.takeDamage(enemy.attack);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Enemy enemy = collision.collider.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            playerInfo.takeDamage(enemy.attack);
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        Debug.Log(SoundManager.Instance.player_attack);
        SoundManager.Instance.playSound(SoundManager.Instance.player_attack);

        Collider2D[] hit = Physics2D.OverlapCircleAll(hitbox.position, attackRange, enemyLayers);

        foreach (Collider2D coll in hit)
        {
            Enemy enemy = coll.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(playerInfo.Attack);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (hitbox == null)
            return;

        Gizmos.DrawWireSphere(hitbox.position, attackRange);
    }
}
