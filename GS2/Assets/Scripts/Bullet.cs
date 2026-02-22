using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] private float alpha = 1f;
    [SerializeField] private Material bulletMaterial;
    private Color color;

    private Animator animator;
    [SerializeField] private AnimationClip destroyClip;

    //[SerializeField] private MeleeEnemy meleeEnemy;
    //[SerializeField] private RangedEnemy rangedEnemy;
    //[SerializeField] private ComboEnemy comboEnemy;

    [SerializeField] private const float BULLET_DAMAGE = 20.0f;
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
            //Debug.Log($"Hit: {collision.gameObject.name}, Layer: {LayerMask.LayerToName(collision.gameObject.layer)}");

            enemy.takeDamage(BULLET_DAMAGE);
        }
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
        {
            //Debug.LogWarning($"Hit: {collision.gameObject.name}, Layer: {LayerMask.LayerToName(collision.gameObject.layer)}");

            player.takeDamage(BULLET_DAMAGE);
        }

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        animator.Play(destroyClip.name);
        //Destroy(gameObject);
    }

    private void updateTransparency()
    {
        color.a = alpha;

        bulletMaterial.color = color;
    }
}
