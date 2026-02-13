using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    enum EnemyType
    {
        Combo,
        Ranged,
        Melee
    }

    private int health;
    //private float range;
    //private float damage;
    private int speed;
    private EnemyType type;

    [SerializeField] private GameObject prefab;

    [SerializeField] private List<GameObject> armList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        armList = new List<GameObject>();
        if (type == EnemyType.Combo)
        {
            health = Random.Range(90, 111);
            speed = 6;
        }
        
        if (type == EnemyType.Ranged)
        {
            health = Random.Range(60, 81);
            speed = Random.Range(3, 6);
        }

        if (type == EnemyType.Melee)
        {
            health = Random.Range(120, 141);
            speed = Random.Range(5, 8);
        }

        foreach (GameObject armObj in armList)
        {
            if (armObj.TryGetComponent<Arm_Base>(out Arm_Base arm))
            {
                arm.DisableIndicator();
                arm.isEnemyArm = true;
            }
        }
    }
    public abstract void takeDamage(float damage);
}