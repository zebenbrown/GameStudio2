using System.Collections.Generic;
using TMPro;
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
        //Debug.Log("Melee Enemy: " + health);

        if (health <= 0)
            die();
    }

    private void die()
    {
        isDead = true;
        
        EnemyManager.instance.RemoveEnemy();
        GameManager.instance.enemiesKilled++;
        dropArms();
        Destroy(gameObject);
    }
    public float getHealth()
    {
        return health;
    }

    protected override void changeAudioVolume()
    {
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.volume = 0.1f;
        }

        else
        {
            Debug.Log("Could not find audio volume on melee enemy");
        }
    }
}
