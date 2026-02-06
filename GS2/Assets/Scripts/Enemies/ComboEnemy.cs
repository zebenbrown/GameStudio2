using UnityEngine;
using UnityEngine.AI;

public class ComboEnemy : MonoBehaviour
{
    private float health;
    private float speed;
    private NavMeshAgent agent;
    private GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = Random.Range(90, 111);
        speed = 6;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
    }
}
