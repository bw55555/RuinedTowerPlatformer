using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int attack = 10;
    public int level = 1;

    private int currentHealth;

    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    
    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        
        Debug.Log("took damage", gameObject);
        currentHealth -= damage;
        //healthBar.SetCurrent(currentHealth, transform.localScale.x < 0);
        //healthBar.ToggleActive(true);
        //Vector2 difference = new Vector2(transform.position.x - player.position.x, 0);
        //difference = difference.normalized * thrust;
        //rb.AddForce(difference);
        GetComponent<EnemyMovement>().Stun(0.3f);

        if (currentHealth <= 0)
        {
            Die();
        }
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
