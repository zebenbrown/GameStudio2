using UnityEngine;
using UnityEngine.InputSystem;

public class PunchScript : Arm_Base
{
    [SerializeField] private AnimationClip punchAnimationClip;
    private string punchAnimationClipName;
    private Animator animator;

    protected override void ArmSpecificStart()
    {
        animator = GetComponent<Animator>();
        punchAnimationClipName = punchAnimationClip.name;
        animator.enabled = false;
    }

    protected override void SpecificEquip()
    {
        animator.enabled = true;
    }

    protected override void SpecificDrop()
    {
        animator.enabled = false;
    }

    private void PunchForward()
    {
        animator.Play(punchAnimationClipName);
    }

    public override void ArmMainAction()
    {
        PunchForward();
    }
}