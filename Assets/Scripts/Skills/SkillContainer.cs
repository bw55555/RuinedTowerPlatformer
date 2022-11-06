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

        //cooldowns in seconds
        addSkill(SkillType.Attack, 0.6f);
        addSkill(SkillType.DoubleJump, 0.4f);
        
        addSkill(SkillType.Dash, 2f);
        addSkill(SkillType.FeatherFall, 20f);
        addSkill(SkillType.Thornmail, 0f);
        addSkill(SkillType.Invincibility, 100f, 0);
        addSkill(SkillType.Extra_Damage, 0f);
        addSkill(SkillType.Key, 0f, 0);
        addSkill(SkillType.Health_Potion, 60f, 0);

        getSkill(SkillType.Attack).Active = true;
        getSkill(SkillType.DoubleJump).Active = true;
    }

    void Start()
    {
        
    }

    void addSkill(SkillType t, float cooldown, int charges = -1)
    {
        skills[(int) t] = new Skill((int)t, cooldown, charges);
    }

    public Skill getSkill(SkillType t)
    {
        return skills[(int) t];
    }

    public bool isSkillReady(SkillType t)
    {
        return getSkill(t).isReady();
    }

    public void useSkill(SkillType t)
    {
        getSkill(t).useSkill();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
