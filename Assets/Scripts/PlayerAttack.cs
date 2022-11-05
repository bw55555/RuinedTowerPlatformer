using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform hitbox;
    public float attackRange = 1f;
    public LayerMask enemyLayers;
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("es");
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hit = Physics2D.OverlapCircleAll(hitbox.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hit)
        {

        }
    }

    void OnDrawGizmosSelected()
    {
        if (hitbox == null)
            return;

        Gizmos.DrawWireSphere(hitbox.position, attackRange);
    }
}
