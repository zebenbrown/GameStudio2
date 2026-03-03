using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    protected GameObject player;
    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] protected List<Arm_Base> armList;
    protected float health;
    protected float speed;
    protected bool isDead = false;

    protected NavMeshAgent agent;

    [SerializeField] protected float attackCooldown;
    [SerializeField] protected float attackCooldownTimer;
    [SerializeField] protected float attackDistance;

    void Start()
    {
        health = Random.Range(60, 81);
        speed = Random.Range(3, 6);
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        player = GameObject.FindGameObjectWithTag("Player");

        armList = new List<Arm_Base>();

        armList = GetComponentsInChildren<Arm_Base>().ToList<Arm_Base>();

        deactivateArmPickup();
        changeAudioVolume();
    }

    private void Update()
    {
        if (isDead) return;

        healthText.text = "Health: " + health;
        agent.SetDestination(player.transform.position);

        if (attackCooldownTimer != 0.0f)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < attackDistance)
            {
                if (attackCooldown == 0.0f)
                {
                    attackPlayer();
                    attackCooldown = attackCooldownTimer;
                }
            }
            if (attackCooldown > 0.0f)
            {
                attackCooldown -= Time.deltaTime;
            }
            else if (attackCooldown < 0.0f)
            {
                attackCooldown = 0.0f;
            }
        }

        //Debug.Log(gameObject.name + " Y Position: " + transform.position.y);


        enemySpecificUpdate();
    }

    protected /*override*/ void attackPlayer()
    {
        foreach (Arm_Base arm in armList)
        {
            arm.armMainAction();
        }
    }

    protected abstract void enemySpecificUpdate();
    public abstract void takeDamage(float damage);
    
    protected abstract void changeAudioVolume();

    private void deactivateArmPickup()
    {
        foreach (Arm_Base arm in armList)
        {
            arm.disableIndicator();
            arm.isEnemyArm = true;
        }
    }
}