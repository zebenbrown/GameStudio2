using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private List<GameObject> armList;
    protected float health;
    protected float speed;
    protected bool isDead = false;

    protected NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        armList = new List<GameObject>();

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