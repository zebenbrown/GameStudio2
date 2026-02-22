using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : Enemy
{

    protected override void enemySpecificUpdate()
    {
        
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
        
        EnemyManager.instance.RemoveEnemy();
        GameManager.instance.enemiesKilled++;
        Destroy(gameObject);
    }
    public float getHealth()
    {
        return health;
    }
}
