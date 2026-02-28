using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    [SerializeField] private List<GameObject> armList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        armList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
