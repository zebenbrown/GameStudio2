using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject player;

    public float currentHealth;
    public float maxHealth = 150f;

    public Image healthBar;

    void Awake()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        currentHealth = player.GetComponent<PlayerController>().health;
        healthBar.fillAmount = (currentHealth /  maxHealth);
    }
}
