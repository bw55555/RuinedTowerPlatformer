using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        gameObject.GetComponent<Image>().enabled = s.Active;
        cooldown.transform.localScale = new Vector3(1, Mathf.Max(0f, s.CurrentCooldown / s.Cooldown), 1);

    }

    private void FixedUpdate()
    {
        Skill s = SkillContainer.Instance.getSkill(type);
        s.CurrentCooldown = s.CurrentCooldown - Time.deltaTime;
    }
}
