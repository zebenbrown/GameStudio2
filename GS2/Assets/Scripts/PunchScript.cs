using UnityEngine;
using UnityEngine.InputSystem;

public class PunchScript : Arm_Base
{
    [SerializeField] private AnimationClip punchAnimationClip;
    private string punchAnimationClipName;
    private Animator animator;

    private AudioSource audioSource;
    private float animationTimer = 0;

    protected override void ArmSpecificStart()
    {
        animator = GetComponent<Animator>();
        punchAnimationClipName = punchAnimationClip.name;
        animator.enabled = false;

        audioSource = GetComponent<AudioSource>();
    }

    protected override void SpecificEquip()
    {
        animator.enabled = true;
    }

    protected override void SpecificDrop()
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

        if (!audioSource.isPlaying && animationTimer == 0)
        {
            audioSource.Play();
            animationTimer = punchAnimationClip.length;
        }
    }

    public override void ArmMainAction()
    {
        PunchForward();
    }
}