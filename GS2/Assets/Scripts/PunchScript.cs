using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PunchScript : Arm_Base
{
    [SerializeField] private AnimationClip punchAnimationClip;
    
    private string punchAnimationClipName;
    private Animator animator;
    private float animationTimer = 0;

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
        animator.Play(punchAnimationClipName);

        playActivateSound();
    }

    public override void armMainAction()
    {
        PunchForward();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isEnemyArm)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.takeDamage(34);
            }
        }
        else
        {
            
            if (other.gameObject == gameManager.getPlayer().gameObject)
            {

            }   
        }   
    }

    protected override void playActivateSound()
    {
        if (animationTimer == 0)
        {
            audioSource.Play();
            animationTimer = punchAnimationClip.length;
        }
    }
}