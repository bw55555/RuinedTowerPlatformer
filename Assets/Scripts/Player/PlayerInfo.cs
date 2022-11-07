using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

    private float iframe_secs = 0.5f;

    private float maxHealth = 100;
    private float currentHealth = 100;
    private float xp = 0;
    private int level = 1;
    private int score = 0;
    private float iframes = 0;
    private int attack = 20;

    public ProgressBar healthBar;

    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public float Xp { get => xp; set => xp = value; }
    public int Level { get => level; set => level = value; }
    public int Score { get => score; set => score = value; }
    public int Attack { get => attack; set => attack = value; }

    //code for fall damage
    private Rigidbody2D rb2D;
    private float speedBeforeLanding;
    private float GetVerticalSpeed() => rb2D.velocity.y;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void setIframes(float iframes)
    {
        this.iframes = iframes;
    }
    public void setCurrentHealth(float health)
    {
        currentHealth = health;
        healthBar.setValue(currentHealth);
    }
    void respawn()
    {
        currentHealth = maxHealth;
        healthBar.setMaxValue(maxHealth);
        xp = 0;
        level = 1;
        score = 0;
    }

    public void addxp(float xp)
    {
        
        this.xp += xp;
        Debug.Log(this.xp + " " + levelUpCost());
        while (this.xp >= levelUpCost())
        {
            levelUp();

        }
    }

    float levelUpCost()
    {
        return level * (level + 1)/2;
    }

    void levelUp()
    {
        Debug.Log("Leveled Up!");
        this.xp -= levelUpCost();
        this.level += 1;

        this.maxHealth += 10;
        this.currentHealth = maxHealth;
        this.attack += 2;

        healthBar.setMaxValue(maxHealth);
    }

    public bool takeDamage(float damage)
    {
        if (iframes > 0) { return false; }
        iframes = iframe_secs;
        SoundManager.Instance.playSound(SoundManager.Instance.player_hit);
        currentHealth -= damage;
        healthBar.setValue(currentHealth);
        return true;
    }

    private void Start()
    {
        healthBar.setMaxValue(maxHealth);
    }
    // Update is called once per frame
    void Update()
    {
        if (currentHealth > maxHealth)
        {
            healthBar.setValue(currentHealth);
            currentHealth = maxHealth;
        }
        //code for Fall Damage
         if (GetVerticalSpeed() < 0)
        {
        speedBeforeLanding = GetVerticalSpeed();
        }

        if (speedBeforeLanding < -20 && speedBeforeLanding > -30 && GetVerticalSpeed() == 0) {
             takeDamage(maxHealth/10);
             speedBeforeLanding = 0;
        }     
        if (speedBeforeLanding < -30 && GetVerticalSpeed() == 0) {
             takeDamage(maxHealth/3);
             speedBeforeLanding = 0;
        }     
        if (currentHealth <= 0)
        {
            Die();
            
        }
    }

    private void FixedUpdate()
    {
        if (iframes > 0)
        {
            iframes -= Time.deltaTime;
        }
        
    }

    void Die()
    {
        if (SkillContainer.Instance.isSkillReady(SkillType.Invincibility))
        {
            SkillContainer.Instance.useSkill(SkillType.Invincibility);
            currentHealth = maxHealth / 2;
            healthBar.setValue(currentHealth);
            //do not die, heal to half?
            return; 
        }
        //death logic here
        score = 0;
    }
}
