using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickSkills : MonoBehaviour
{
    public GameObject origin;
    public Image first;
    public Image second;
    public Image third;
    public Image fourth;
    public Image fifth;
    public Image sixth;
    public Image seventh;
    public Image eighth;

    public Image one;
    public Image two;
    public Image three;
    private int[] options = new int[3];
    void OnEnable()
    {
        Image[] icons = new Image[] { first, second, third, fourth, fifth, sixth, seventh, eighth };
        int count = 0;
        int selection;
        while(count < 3)
        {
            selection = Random.Range(0, 8);

            if (!SkillContainer.Instance.getSkill((SkillType)selection).Active)
            {
                if (count == 0)
                {
                    one.sprite = icons[selection - 1].sprite;
                }
                else if (count == 1)
                {
                    two.sprite = icons[selection - 1].sprite;
                }
                else
                {
                    three.sprite = icons[selection - 1].sprite;
                }
                options[count] = selection;
                count += 1;
            }
        }

        
    }

    public void EquipSkill(int i)
    {
        SkillContainer.Instance.getSkill((SkillType)options[i]).Active = true;
        origin.SetActive(false);
        this.enabled = false;
    }
}
