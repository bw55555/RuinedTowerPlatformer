using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlot : MonoBehaviour
{
    public SkillType type;
    public GameObject cooldown;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        Skill s = SkillContainer.Instance.getSkill(type);
        s.decreaseCooldown();
        gameObject.SetActive(s.Active);
        cooldown.transform.localScale = new Vector3(1, Mathf.Max(0f, s.CurrentCooldown / s.Cooldown), 1);

    }
}
