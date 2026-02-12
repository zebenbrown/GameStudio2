using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 8.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Hit: {collision.gameObject.name}, Layer: {LayerMask.LayerToName(collision.gameObject.layer)}");
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController.takeDamage(20);
        }

        if (collision.gameObject.CompareTag("MeleeEnemy"))
        {
            MeleeEnemy.takeDamage(20);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("RangedEnemy"))
        {
            RangedEnemy.takeDamage(20);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("ComboEnemy"))
        {
            ComboEnemy.takeDamage(20);
            Destroy(gameObject);
        }
    }
}
