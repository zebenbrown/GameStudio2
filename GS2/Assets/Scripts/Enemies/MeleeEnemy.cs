using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    private float health;
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
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + health;
        agent.SetDestination(player.transform.position);
    }
}
