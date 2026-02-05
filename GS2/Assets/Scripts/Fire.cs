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

    const float forwardForceFloat = 1000;
    Vector3 forwardForceVector;


    protected override void ArmSpecificStart()
    {
        startRotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);

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

        bullet.GetComponent<Rigidbody>().AddForce(forwardForceVector);
        bullet.transform.SetParent(null);

        audioSource.generator = GetRandomShootSound();
        audioSource.Play();
    }

    private AudioClip GetRandomShootSound()
    {
        AudioClip sound = shootSounds.ElementAt((int)Random.Range(0.0f, shootSounds.Count));

        return sound;
    }
}
