using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private List<Vector3> spawnBoundries = new List<Vector3>();
    private static int enemyCount;
    [SerializeField] private List<GameObject> enemyPrefabList = new List<GameObject>();
    
    //Spawn Bounds
    float minX = -50, maxX = 50;
    private float minZ = -50, maxZ = 50;

    enum enemyType
    {
        Combo,
        Ranged,
        Melee
    }
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnEnemy(enemyType.Combo);
        spawnEnemy(enemyType.Ranged);
        spawnEnemy(enemyType.Melee);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCount < 3)
        {
            //StartCoroutine(Co_Delay(3));
            int type = Random.Range(0, 3);
            spawnEnemy((enemyType)type);
        }
    }
    
    private void spawnEnemy(enemyType type)
    {
        if (enemyCount < 3)
        {
            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);
            Vector3 randomSpawn = new Vector3(randomX, 1, randomZ);
            if (type == enemyType.Combo)
            {
                enemies.Add(Instantiate(enemyPrefabList[(int)enemyType.Combo], randomSpawn, Quaternion.identity));
                enemyCount++;
            }

            if (type == enemyType.Ranged)
            {
                enemies.Add(Instantiate(enemyPrefabList[(int)enemyType.Ranged], randomSpawn, Quaternion.identity));
                enemyCount++;
            }

            if (type == enemyType.Melee)
            {
                enemies.Add(Instantiate(enemyPrefabList[(int)enemyType.Melee], randomSpawn, Quaternion.identity));
                enemyCount++;
            }
        }
    }

    IEnumerator Co_Delay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public static void RemoveEnemy()
    {
        enemyCount--;
    }
}
