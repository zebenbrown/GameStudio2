using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private MeleeEnemy meleeEnemy;

    [SerializeField] private RangedEnemy rangedEnemy;

    [SerializeField] private ComboEnemy comboEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 5.0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Hit: {collision.gameObject.name}, Layer: {LayerMask.LayerToName(collision.gameObject.layer)}");
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

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.takeDamage(20);
            Destroy(gameObject);
        }
    }
}
