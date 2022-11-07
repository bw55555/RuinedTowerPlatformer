using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int attack = 10;
    public int level = 1;

    private int currentHealth;

    private float flashTime = 0f;

    private float maxFlashTime = 0.3f;
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
        flashTime = maxFlashTime;
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

    private void FixedUpdate()
    {
        if (flashTime > 0)
        {
            flashTime -= Time.deltaTime;
            SpriteRenderer s = gameObject.GetComponent<SpriteRenderer>();
            s.color = new Color(238, 75, 43);
            if (flashTime <= 0)
            {
                s.color = new Color(255, 255, 255);
            }
        } 
    }
}
