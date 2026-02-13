using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PartDetection : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerController player;

    [SerializeField] private InputActionReference swapArmAction_R;
    [SerializeField] private InputActionReference swapArmAction_L;

    [SerializeField] private ArmSocketScript armSocket_R;
    [SerializeField] private ArmSocketScript armSocket_L;

    public List<Arm_Base> ArmsInRange;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        player = GetComponentInParent<PlayerController>();

        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnEnable()
    {
        swapArmAction_R.action.performed += swapRightArm;
        swapArmAction_L.action.performed += swapLeftArm;
    }
    private void OnDisable()
    {
        swapArmAction_R.action.performed -= swapRightArm;
        swapArmAction_L.action.performed -= swapLeftArm;
    }

    private void swapRightArm(InputAction.CallbackContext context)
    {
        swapArm(armSocket_R);
    }
    private void swapLeftArm(InputAction.CallbackContext context)
    {
        swapArm(armSocket_L);
    }

    public void swapArm(ArmSocketScript armSocket)
    {
        if (ArmsInRange.Count > 0)
        {
            armSocket.GrabArm(getClosestArm());
        }
        else
        {
            armSocket.DropArm();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Arm_Base target))
        {
            if (!target.isEnemyArm)
            {
                addArm(target);
                //Debug.Log("(Detector) Added Arm: " + target.name + "\nArms in range: " + ArmsInRange.Count);

                SetIndicatorMaterial(true, target);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Arm_Base target))
        {
            if (!target.isEnemyArm)
            {
                removeArm(target);
                //Debug.Log("(Detector) Removed Arm: " + target.name + "\nArms in range: " + ArmsInRange.Count);

                SetIndicatorMaterial(false, target);
            }
        }
    }

    public void addArm(Arm_Base arm)
    {
        ArmsInRange.Add(arm);
        //Debug.Log("Arm Added: " + arm.name);
    }

    public void removeArm(Arm_Base arm)
    {
        ArmsInRange.Remove(arm);
        //Debug.Log("Arm Removed: " + arm.name);
    }

    public void SetIndicatorMaterial(bool Opaque, Arm_Base arm)
    {
        if (Opaque)
        {
            arm.SetIndiatorMat(gameManager.GetMaterial("LightBlue"));
        }
        else
        {
            arm.SetIndiatorMat(gameManager.GetMaterial("LightBlue_Transparent"));
        }
    }

    private Arm_Base getClosestArm()
    {
        if (ArmsInRange.Count == 0)
        {
            return null;
        }

        Arm_Base closestArm = null;

        foreach (Arm_Base arm in ArmsInRange)
        {
            if (closestArm == null)
            {
                closestArm = arm;
            }
            else
            {
                if (Vector3.Distance(closestArm.transform.position, transform.position) > Vector3.Distance(arm.transform.position, transform.position))
                {
                    closestArm = arm;
                }
            }
        }

        return closestArm;
    }
}