using System.Collections.Generic;
using UnityEngine;

public class PunchScript : Arm_Base
{
    [SerializeField] private AnimationClip punchAnimationClip;

    [SerializeField] private float PUNCH_DAMAGE = 34;

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

        audioSource = gameObject.GetComponent<AudioSource>();
        changeAudioVolume();
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
            EndPunchAction();
            animationTimer = 0;
        }
    }

    private void EndPunchAction()
    {
        punchStarted = false;
        animator.enabled = false;
        damageDealt = false;
    }

    private void PunchForward()
    {
        if (!punchStarted)
        {
            animator.enabled = true;
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
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Hit");
        }
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
                        Debug.Log("Punch Damage: " + arm.damage);
                        enemy.takeDamage(arm.damage);
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

            if (other.gameObject.name != "PartDetector")
            {
                if (other.transform.parent == null)
                {
                    if (other.TryGetComponent<PlayerController>(out PlayerController player))
                    {
                        player.takeDamage(PUNCH_DAMAGE);
                        damageDealt = true;
                    }
                }
                else if (other.transform.parent.TryGetComponent<PlayerController>(out PlayerController player))//.TryGetComponent<PlayerController>(out PlayerController player))
                {
                    if (punchStarted && !damageDealt)
                    {
                        player.takeDamage(PUNCH_DAMAGE);
                        damageDealt = true;
                    }
                }
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

    private void changeAudioVolume()
    {
        audioSource.volume = 0.3f;
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
                        enemy.takeDamage(PUNCH_DAMAGE);
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