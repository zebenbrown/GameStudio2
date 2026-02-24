using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private List<Vector3> spawnBoundries = new List<Vector3>();
    private int enemyCount;
    [SerializeField] private List<GameObject> enemyPrefabList = new List<GameObject>();

    public static EnemyManager instance;
    
    //Spawn Bounds
    float minX = -33, maxX = 33;
    private float minZ = -1.5f, maxZ = 42.5f;

    enum enemyType
    {
        Combo,
        Ranged,
        Melee
    }

    private void Awake()
    {
        instance = this;
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

            Vector3 randomSpawn = RandomPointInQuad(spawnBoundries[0], spawnBoundries[1], spawnBoundries[2], spawnBoundries[3]);
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

    Vector3 RandomPointInQuad(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        // Choose which triangle to choose from
        if (Random.value < 0.5f)
            return RandomPointInTriangle(a, b, c);
        else
            return RandomPointInTriangle(a, c, d);
    }
    Vector3 RandomPointInTriangle(Vector3 a, Vector3 b, Vector3 c)
    {
        float r1 = Random.value;
        float r2 = Random.value;

        if (r1 + r2 > 1f)
        {
            r1 = 1f - r1;
            r2 = 1f - r2;
        }

        return a + r1 * (b - a) + r2 * (c - a);
    }

    IEnumerator Co_Delay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public void RemoveEnemy()
    {
        enemyCount--;
    }
}
