using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    private static float health;
    private float speed;

    private NavMeshAgent agent;
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI healthText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = Random.Range(120, 141);
        speed = Random.Range(5, 8);
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        player = GameObject.FindGameObjectWithTag("Player");

        deactivateArmPickup();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + health;
        agent.SetDestination(player.transform.position);

        if (health <= 0)
        {
            EnemyManager.RemoveEnemy();
            GameManager.enemiesKilled++;
            Destroy(gameObject);
        }
    }

    public static void takeDamage(float damage)
    {
        health -= damage;
    }

    private void deactivateArmPickup()
    {
        Arm_Base[] armList = GetComponentsInChildren<Arm_Base>();

        foreach (Arm_Base arm in armList)
        {
            arm.DisableIndicator();
            arm.isEnemyArm = true;
        }
    }
}
