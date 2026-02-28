using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PunchScript : Arm_Base
{
    [SerializeField] private AnimationClip punchAnimationClip;

    private string punchAnimationClipName;
    private Animator animator;
    private float animationTimer = 0;
    private bool punchStarted = false;
    private bool damageDealt = false;
    private Dictionary<GameObject, bool> enemiesHit = new Dictionary<GameObject, bool>();

    [SerializeField] private AudioClip punchAudio;

    protected override void armSpecificStart()
    {
        animator = GetComponent<Animator>();
        punchAnimationClipName = punchAnimationClip.name;
        animator.enabled = false;

        collider = GetComponentInChildren<Collider>();
    }

    protected override void specificEquip()
    {
        animator.enabled = true;
    }

    protected override void specificDrop()
    {
        animator.enabled = false;
    }

    private void Update()
    {
        if (animationTimer > 0)
        {
            animationTimer -= Time.deltaTime;
        }
        else
        {
            animationTimer = 0;
        }
    }

    private void PunchForward()
    {
        if (!punchStarted)
        {
            animator.Play(punchAnimationClipName);

            playActivateSound();

            punchStarted = true;
        }
    }

    public override void armMainAction()
    {
        PunchForward();
    }

    private void OnTriggerEnter(Collider other)
    {
        //basically is it the player's arm
        if (!isEnemyArm)
        {
            if (punchStarted)
            {
                if (!damageDealt)
                {
                    Enemy enemy = other.gameObject.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.takeDamage(34);
                        damageDealt = true;
                    }
                    
                    /*if (other.gameObject.GetComponent<Enemy>())
                    {
                        //Dictionary<enemy GameObject, enemy already took damage?>
                        //enemiesHit.Add(other.gameObject, false);


                    }*/
                }
            }
        }
        else
        {
            if (other.gameObject == gameManager.getPlayer().gameObject)
            {
            }
        }
        //enemiesHit.Clear();
    }

    protected override void playActivateSound()
    {
        if (animationTimer == 0)
        {
            audioSource.Play();
            animationTimer = punchAnimationClip.length;
        }
    }

    public void animationOver()
    {
        punchStarted = false;
        damageDealt = false;
    }

    /*private void dealDamage()
    {
        if (punchStarted)
        {
            foreach (KeyValuePair<GameObject, bool> entry in enemiesHit)
            {
                if (entry.Value == false)
                {
                    Enemy enemy = entry.Key.gameObject.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.takeDamage(34);
                        if (enemiesHit.ContainsKey(enemy.gameObject))
                        {
                            enemiesHit.Remove(entry.Key);
                            enemiesHit.Add(enemy.gameObject, true);
                        }
                    }
                }
            }
        }
    }*/
}