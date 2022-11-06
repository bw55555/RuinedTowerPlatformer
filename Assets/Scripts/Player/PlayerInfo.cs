﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

    public static float m_iframes = 30f;
    public static PlayerInfo Instance;

    private int maxHealth = 100;
    private int currentHealth = 100;
    private int xp = 0;
    private int level = 1;
    private int score = 0;
    private float iframes = 0;
    private int attack = 5;
    //add health bar later

    private void Awake()
    {
        Instance = this;
    }

    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public int Xp { get => xp; set => xp = value; }
    public int Level { get => level; set => level = value; }
    public int Score { get => score; set => score = value; }
    public int Attack { get => attack; set => attack = value; }

    void respawn()
    {
        currentHealth = maxHealth;
        xp = 0;
        level = 1;
        score = 0;
    }

    public void addxp(int xp)
    {
        this.xp += xp;
        while (this.xp < levelUpCost())
        {
            levelUp();
        }
    }

    int levelUpCost()
    {
        return 100 * level;
    }

    void levelUp()
    {
        this.xp -= levelUpCost();
        this.level += 1;

        this.maxHealth += 10;
        this.currentHealth = maxHealth;
        this.attack += 2;
    }

    public void takeDamage(int damage)
    {
        if (iframes > 0) { return; }
        iframes = m_iframes;
        currentHealth -= damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (iframes > 0)
        {
            iframes -= 1f;
        }
    }
}