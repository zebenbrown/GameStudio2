using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fire : Arm_Base
{
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private Transform BulletReleasePoint;

    [SerializeField] private List<AudioClip> shootSounds;

    const float forwardForceFloat = 25;
    Vector3 forwardForceVector;


    protected override void armSpecificStart()
    {
        startRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        forwardForceVector = Vector3.forward;
        forwardForceVector.z += forwardForceFloat;

        audioSource = GetComponent<AudioSource>();
    }

    public override void armMainAction()
    {
        shootGun();

        playActivateSound();
    }

    protected override void specificEquip()
    {
        transform.localPosition = Vector3.zero;
    }

    protected override void specificDrop()
    {
    }

    public void shootGun()
    {
        GameObject bullet = Instantiate(BulletPrefab, BulletReleasePoint);

        if (!isEnemyArm)
        {
            bullet.GetComponent<Bullet>().isPlayers = true;
        }
        else
        {
            bullet.GetComponent<Bullet>().enemySource = GetComponentInParent<Enemy>();
        }

        bullet.GetComponent<Rigidbody>().AddForce(BulletReleasePoint.forward * forwardForceFloat, ForceMode.Impulse);
        Collider bulletCollider = bullet.GetComponent<Collider>();
        Collider playerCollider = bullet.GetComponentInParent<Collider>();

        Physics.IgnoreCollision(bulletCollider, playerCollider);
        bullet.transform.SetParent(null);
    }

    private AudioClip getRandomShootSound()
    {
        AudioClip sound = shootSounds.ElementAt((int)Random.Range(0.0f, shootSounds.Count));

        return sound;
    }

    protected override void playActivateSound()
    {
        audioSource.clip = getRandomShootSound();
        audioSource.volume = activationVolume;
        audioSource.Play();
    }
}