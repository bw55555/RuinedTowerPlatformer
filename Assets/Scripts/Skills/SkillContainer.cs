using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    DoubleJump = 0,
    Dash = 1,
    FeatherFall = 2,

}

public class SkillContainer : MonoBehaviour
{
    // Start is called before the first frame update

    public int numSkills = 3;
    private Skill[] skills;
    public static SkillContainer Instance;

    private void Awake()
    {
        Instance = this;
        skills = new Skill[numSkills];
        addSkill(SkillType.DoubleJump, 120f);
        addSkill(SkillType.Dash, 120f);
        addSkill(SkillType.FeatherFall, 120f);
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
