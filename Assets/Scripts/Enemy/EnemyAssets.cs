using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAssets : MonoBehaviour
{
    // Start is called before the first frame update
    public static EnemyAssets Instance;

    public GameObject enemyDeath;

    public GameObject skeleton;
    public GameObject slime;
    public GameObject knight;
    public GameObject demon;

    private void Awake()
    {
        Instance = this;
    }

    public void createEnemy(EnemyType type, float xpos, float ypos)
    {
        switch (type)
        {
            case EnemyType.Slime: Instantiate(slime, new Vector3(xpos + 0.5f, ypos + 0.5f, 0), Quaternion.identity); return;
            case EnemyType.Skeleton: Instantiate(skeleton, new Vector3(xpos + 0.5f, ypos + 1.5f, 0), Quaternion.identity); return;
            case EnemyType.Knight: Instantiate(knight, new Vector3(xpos + 1f, ypos, 0), Quaternion.identity); return;
            case EnemyType.Demon: Instantiate(demon, new Vector3(xpos + 1f, ypos, 0), Quaternion.identity); return;
        }
    }
}
