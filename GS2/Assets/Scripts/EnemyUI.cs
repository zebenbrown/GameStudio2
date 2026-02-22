using System;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    GameObject cameraTarget = null;
    Camera camera = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraTarget = GameObject.FindGameObjectWithTag("CameraTarget");

        if (cameraTarget == null)
        {
            Debug.LogWarning("No Camera Target Found");
        }
        camera = cameraTarget.gameObject.GetComponentInChildren<Camera>();

        if (camera == null)
        {
            Debug.LogWarning("No Camera Found");
        }
        
        
    }

    private void LateUpdate()
    {
        if (camera != null)
        {
            transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
        }
    }
}
