using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    private int id;
    private bool active;
    private float cooldown;

    public Skill(int id, float cooldown)
    {
        this.id = id;
        active = false;
        this.cooldown = cooldown;
        this.currentCooldown = 0f;
    }

    public void decreaseCooldown()
    {
        this.currentCooldown -= 1f;
    }

    public void useSkill()
    {
        this.currentCooldown = cooldown;
    }

    public bool isReady()
    {
        return active && this.currentCooldown <= 0;
    }

    private float currentCooldown;

    public int Id { get => id; set => id = value; }
    public bool Active { get => active; set => active = value; }
    public float Cooldown { get => cooldown; set => cooldown = value; }
    public float CurrentCooldown { get => currentCooldown; set => currentCooldown = value; }
}
