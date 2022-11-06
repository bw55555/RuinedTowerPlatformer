using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    Attack = 0,
    DoubleJump = 1,
    Dash = 2,
    FeatherFall = 3,
    Thornmail = 4,
    Invincibility = 5,
    Extra_Damage = 6,
    Key = 7,
    Health_Potion = 8
}

public class SkillContainer : MonoBehaviour
{
    // Start is called before the first frame update

    public int numSkills = 10;
    private Skill[] skills;
    public static SkillContainer Instance;

    private void Awake()
    {
        Instance = this;
        skills = new Skill[numSkills];
        addSkill(SkillType.Attack, 70.0f);
        addSkill(SkillType.DoubleJump, 120f);
        
        addSkill(SkillType.Dash, 120f);
        addSkill(SkillType.FeatherFall, 120f);

        getSkill(SkillType.Attack).Active = true;
    }

    void Start()
    {
        
    }

    void addSkill(SkillType t, float cooldown)
    {
        skills[(int) t] = new Skill((int)t, cooldown);
    }

    public Skill getSkill(SkillType t)
    {
        return skills[(int) t];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
