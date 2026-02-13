using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : MonoBehaviour
{
    private static float health;
    private float speed;
    private NavMeshAgent agent;
    private GameObject player;

    [SerializeField] private TextMeshProUGUI healthText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = Random.Range(60, 81);
        speed = Random.Range(3, 6);
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + health;
        agent.SetDestination(player.transform.position);
        if (agent.remainingDistance <= 20)
        {
           agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }

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
}
