using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy : MonoBehaviour
{
    public float base_health = 100;
    public float health_scaling = 10;
    public float base_attack = 10;
    public float attack_scaling = 2;
    public int level = 1;

    private float maxHealth;

    private float attack;

    private float currentHealth;

    private float flashTime = 0f;

    private float maxFlashTime = 0.3f;
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public float Attack { get => attack; set => attack = value; }

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
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        flashTime = maxFlashTime;
        GetComponent<EnemyMovement>().Stun(0.3f);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public float xpOnDeath()
    {
        return level + 1;
    }

    void Die()
    {
        Debug.Log("You killed an enemy");
        //healthBar.ToggleActive(false);
        //anim.SetTrigger("Dead");
        SoundManager.Instance.playSound(SoundManager.Instance.enemy_death);
        GetComponent<Collider2D>().enabled = false;
        Instantiate(EnemyAssets.Instance.enemyDeath, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }

    public bool isDead()
    {
        return currentHealth <= 0;
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
