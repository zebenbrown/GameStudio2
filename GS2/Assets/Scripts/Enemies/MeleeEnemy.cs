using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : Enemy
{
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
        if (isDead) return;
        
        healthText.text = "Health: " + health;
        agent.SetDestination(player.transform.position);
    }

    public override void takeDamage(float damage)
    {
        if (isDead) return;
        
        health -= damage;
        Debug.Log("Melee Enemy: " + health);

        if (health <= 0)
            die();
    }

    private void die()
    {
        isDead = true;
        
        EnemyManager.RemoveEnemy();
        GameManager.enemiesKilled++;
        Destroy(gameObject);
    }
    public float getHealth()
    {
        return health;
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
