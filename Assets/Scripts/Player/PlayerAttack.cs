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
        if (enemy != null)
        {
            enemyAttack(enemy);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Enemy enemy = collision.collider.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemyAttack(enemy);
        }
    }

    void enemyAttack(Enemy enemy)
    {
        bool tookDamage = playerInfo.takeDamage(enemy.Attack);
        if (tookDamage && SkillContainer.Instance.isSkillReady(SkillType.Thornmail))
        {
            SkillContainer.Instance.useSkill(SkillType.Thornmail);
            enemy.TakeDamage(enemy.Attack / 2);
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        SoundManager.Instance.playSound(SoundManager.Instance.player_attack);

        Collider2D[] hit = Physics2D.OverlapCircleAll(hitbox.position, attackRange, enemyLayers);

        foreach (Collider2D coll in hit)
        {
            Enemy enemy = coll.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                float critMultiplier = 1;
                if (SkillContainer.Instance.isSkillReady(SkillType.Extra_Damage) && Random.Range(0, 5) == 0)
                {
                    critMultiplier = 1.5f;
                }
                enemy.TakeDamage(playerInfo.Attack * critMultiplier);
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
