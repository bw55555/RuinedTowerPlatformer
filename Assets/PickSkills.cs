using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickSkills : MonoBehaviour
{
    SkillContainer current;
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
    void OnEnable()
    {
        Image[] icons = new Image[] { first, second, third, fourth, fifth, sixth, seventh, eighth };
        int count = 0;
        int selection;
        while(count < 3)
        {
            selection = Random.Range(1, 9);
            Debug.Log(selection);
            Debug.Log(current.getSkill((SkillType)selection).Active);

            if (!current.getSkill((SkillType)selection).Active)
            {
                if (count == 0)
                {
                    Debug.Log("got one");
                    one.sprite = icons[selection].sprite;
                }
                else if (count == 1)
                {
                    two.sprite = icons[selection].sprite;
                }
                else
                {
                    three.sprite = icons[selection].sprite;
                }
                count += 1;
            }
        }

        
    }
}
