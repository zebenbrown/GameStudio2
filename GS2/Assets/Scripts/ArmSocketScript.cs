using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmSocketScript : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private ArmSocketScript otherArmSocket;
    private Arm_Base AttachedArm;
    public List<Arm_Base> ArmsInRange;
    private GameObject ArmDetector;

    [SerializeField] private InputActionReference swapArmAction;
    [SerializeField] InputActionReference armAction;

    private bool isEquipped;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        gameManager.RegisterSocket(this);

        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnEnable()
    {
        swapArmAction.action.performed += SwapArm;
        armAction.action.performed += ActivateArmAction;
    }

    private void OnDisable()
    {
        swapArmAction.action.performed -= SwapArm;
        armAction.action.performed -= ActivateArmAction;
    }

    public void SwapArm(InputAction.CallbackContext context)
    {
        if(isEquipped)
        {
            DropArm();
        }
        else
        {
            DetectArm();
        }
    }
    private void ActivateArmAction(InputAction.CallbackContext context)
    {
        if (AttachedArm != null)
        {
            AttachedArm.ArmMainAction();
        }
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

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Arm_Base target))
        {
            if (IsNotInOtherList(target))
            {
                SetIndicatorMaterial(true, target);
            }
            ArmsInRange.Add(target);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Arm_Base target))
        {
            if (IsNotInOtherList(target))
            {
                SetIndicatorMaterial(false, target);
            }
            ArmsInRange.Remove(target);
        }
    }

    private Arm_Base GetClosestArm()
    {
        if (ArmsInRange.Count == 0)
        {
            return null;
        }

        Arm_Base closestArm = null;

        foreach(Arm_Base arm in ArmsInRange)
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

    private bool IsNotInOtherList(Arm_Base targetArm)
    {
        bool IsValid = true;

        foreach(Arm_Base arm in otherArmSocket.ArmsInRange)
        {
            if (arm == targetArm)
            {
                IsValid = false;
            }
        }

        return IsValid;
    }

    public void DetectArm()
    {
        if (ArmsInRange.Count != 0)
        {
            GrabArm();
        }
    }

    public void GrabArm()
    {
        AttachedArm = GetClosestArm();
        ArmsInRange.Remove(AttachedArm);
        if (otherArmSocket.ArmsInRange.Contains(AttachedArm))
        {
            otherArmSocket.ArmsInRange.Remove(AttachedArm);
        }

        AttachedArm.EquipArm(transform);

        isEquipped = true;

        gameManager.CheckEquipStatus();
    }

    private void DropArm()
    {
        AttachedArm.DropArm();
        AttachedArm = null;
        isEquipped = false;

        gameManager.EnableUnequippedIndicators();
    }

    public bool IsEquipped() { return isEquipped; }
}
