using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerate : MonoBehaviour
{
    private GameObject monsterChoice;

    public GameObject knightPrefab;
    public GameObject demonPrefab;
    public GameObject slimePrefab;
    public GameObject skeletonPrefab;
    private void Start()
    {
        if (gameObject.tag.Equals("MonsterRoom"))
        {
            int monsterNum = Random.Range(1, 5);
            if (monsterNum == 1)
            {
                monsterChoice = knightPrefab;
            }
            else if (monsterNum == 2)
            {
                monsterChoice = demonPrefab;
            }
            else if (monsterNum == 3)
            {
                monsterChoice = slimePrefab;
            }
            else if (monsterNum == 4)
            {
                monsterChoice = skeletonPrefab;
            }
            float xPos = gameObject.transform.position.x;
            float yPos = gameObject.transform.position.y;
            Instantiate(monsterChoice, new Vector3(xPos + 1.0f, yPos + 0.5f, 0), Quaternion.identity);
            Instantiate(monsterChoice, new Vector3(xPos + 2.0f, yPos + 0.5f, 0), Quaternion.identity);
            Instantiate(monsterChoice, new Vector3(xPos - 1.0f, yPos + 0.5f, 0), Quaternion.identity);
            Instantiate(monsterChoice, new Vector3(xPos - 2.0f, yPos + 0.5f, 0), Quaternion.identity);
        }
    }
}
