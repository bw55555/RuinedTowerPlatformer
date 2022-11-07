using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy : MonoBehaviour
{
    public int base_health = 100;
    public int health_scaling = 10;
    public int base_attack = 10;
    public int attack_scaling = 2;
    public int level = 1;

    private int maxHealth;

    private int attack;

    private int currentHealth;

    private float flashTime = 0f;

    private float maxFlashTime = 0.3f;
    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public int Attack { get => attack; set => attack = value; }

    private void Awake()
    {
        currentHealth = maxHealth;
        spawn(MainController.Instance.Level);
    }
    
    // Update is called once per frame

    public void spawn(int level)
    {
        this.level = level;
        attack = base_attack + attack_scaling * (level - 1);
        maxHealth = base_health + health_scaling * (level - 1);
        currentHealth = maxHealth;
    }
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
        Instantiate(EnemyAssets.Instance.enemyDeath, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
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
