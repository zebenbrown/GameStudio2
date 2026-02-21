using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fire : Arm_Base
{
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private Transform BulletReleasePoint;

    [SerializeField] private List<AudioClip> shootSounds;
    private AudioSource audioSource;

    const float forwardForceFloat = 25;
    Vector3 forwardForceVector;

    protected override void ArmSpecificStart()
    {
        startRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        forwardForceVector = Vector3.forward;
        forwardForceVector.z += forwardForceFloat;

        audioSource = GetComponent<AudioSource>();
    }

    public override void ArmMainAction()
    {
        ShootGun();
    }

    protected override void SpecificEquip()
    {
        transform.localPosition = Vector3.zero;
    }

    protected override void SpecificDrop()
    {
        
    }

    public void ShootGun()
    {
        GameObject bullet = Instantiate(BulletPrefab, BulletReleasePoint);

        bullet.GetComponent<Rigidbody>().AddForce(BulletReleasePoint.forward * forwardForceFloat, ForceMode.Impulse);
        Collider bulletCollider = bullet.GetComponent<Collider>();
        Collider playerCollider = bullet.GetComponentInParent<Collider>();
        
        Physics.IgnoreCollision(bulletCollider, playerCollider);
        bullet.transform.SetParent(null);

        audioSource.generator = GetRandomShootSound();
        audioSource.Play();
    }

    private void OnLook(InputValue value)
    {
        Vector2 mousePos;
        mousePos = Camera.main.ScreenToWorldPoint(value.Get<Vector2>());
        AimAt(mousePos);
    }

    protected void AimAt(Vector3 target)
    {
        //float lookAngle = target.y;
        //transform.eulerAngles = new Vector3(0, lookAngle, 0);
        //Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, target);
        Quaternion targetRotation = Quaternion.Euler(0, target.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 15f * Time.deltaTime);
    }

    private AudioClip GetRandomShootSound()
    {
        AudioClip sound = shootSounds.ElementAt((int)Random.Range(0.0f, shootSounds.Count));

        return sound;
    }
}
