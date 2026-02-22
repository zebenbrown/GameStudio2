using UnityEngine;
using UnityEngine.InputSystem;

public class PunchScript : Arm_Base
{
    [SerializeField] private AnimationClip punchAnimationClip;
    
    private string punchAnimationClipName;
    private Animator animator;
    private float animationTimer = 0;

    private AudioSource audioSource;

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

    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with: " + collision.gameObject.name);
        
        if (GetComponentInParent<PlayerController>() == null)
        {
            return;
        }

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.takeDamage(34);
        }

        /*if (transform.parent.parent.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("MeleeEnemy") || collision.gameObject.CompareTag("RangedEnemy") ||
                collision.gameObject.CompareTag("ComboEnemy"))
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                    enemy.takeDamage(34);
            }
        }

        else
        {
            return;
        }#1#
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag("EnemyArm"))
        {
            return;
        }
       
        Enemy enemy = other.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            enemy.takeDamage(34);
            Debug.Log("Hit enemy");
        }
    }
}