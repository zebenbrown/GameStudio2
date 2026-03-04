using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] private float alpha = 1f;
    [SerializeField] private Material bulletMaterial;
    private Color color;
    private float damage; //needs to be set in "constructor" the constructor function

    public bool isPlayers = false;
    public Enemy enemySource;

    private Animator animator;
    [SerializeField] private AnimationClip destroyClip;

    //[SerializeField] private const float BULLET_DAMAGE = 20.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        color = bulletMaterial.color;
        animator = GetComponent<Animator>();

        Destroy(gameObject, 5.0f);
    }

    private void Update()
    {
        //updateTransparency();
    }

    void OnCollisionEnter(Collision collision)
    {

        /*if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.takeDamage(20);
        }*/

        /*if (collision.gameObject.CompareTag("MeleeEnemy"))
        {
            meleeEnemy.GetComponent<MeleeEnemy>();
            meleeEnemy.takeDamage(20);
            Destroy(gameObject);
        }


        if (collision.gameObject.CompareTag("RangedEnemy"))
        {
            rangedEnemy.GetComponent<RangedEnemy>();
            rangedEnemy.takeDamage(20);
            Destroy(gameObject);
        }


        if (collision.gameObject.CompareTag("ComboEnemy"))
        {
            comboEnemy.GetComponent<ComboEnemy>();
            comboEnemy.takeDamage(20);
            Destroy(gameObject);
        }*/

        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (enemy != enemySource)
            {
                enemy.takeDamage(damage);
                Destroy(gameObject);
                return;
            }
        }
        else if(collision.transform.parent != null)
        {
            if (collision.transform.parent.TryGetComponent<Enemy>(out Enemy enemyParent))
            {
                if (enemyParent != enemySource)
                {
                    enemyParent.takeDamage(damage);
                    Destroy(gameObject);
                    return;
                }
            }
        }
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
        {
            if (!isPlayers)
            {
                player.takeDamage(damage);
                Destroy(gameObject);
                return;
            }
        }

        if (!collision.gameObject.CompareTag("Floor"))
        {
            if (isPlayers && (collision.gameObject.GetComponentInParent<PlayerController>() == null))
            {
                Destroy(gameObject);
            }
            else if (!isPlayers)
            {
                if (collision.gameObject.GetComponentInParent<Enemy>() != enemySource)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void updateTransparency()
    {
        color.a = alpha;

        bulletMaterial.color = color;
    }

    public void constructor(float damage)
    {
        
    }
}
