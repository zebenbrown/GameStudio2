using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : MonoBehaviour
{
    private float health;
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
        Debug.Log("Distance" + agent.remainingDistance);
        if (agent.remainingDistance <= 20)
        {
           agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }
    }
}
