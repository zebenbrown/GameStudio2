using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class ArmSocketScript : MonoBehaviour
{
    private GameManager gameManager;

    public Arm_Base AttachedArm;
    private PartDetection partDetector;

    [SerializeField] private InputActionReference swapArmAction;
    [SerializeField] InputActionReference armAction;

    private bool isEquipped;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        gameManager.RegisterSocket(this);

        partDetector = transform.parent.GetComponentInChildren<PartDetection>();
    }

    private void OnEnable()
    {
        armAction.action.performed += ActivateArmAction;
    }

    private void OnDisable()
    {
        armAction.action.performed -= ActivateArmAction;
    }

    private void ActivateArmAction(InputAction.CallbackContext context)
    {
        if (AttachedArm != null)
        {
            AttachedArm.armMainAction();
        }
    }

    public void GrabArm(Arm_Base arm)
    {
        if (AttachedArm != null)
        {
            AttachedArm.dropArm();
            AttachedArm.transform.position += new Vector3(0.0f, 1.0f, 0.0f);

            //partDetector.addArm(AttachedArm);
            //Debug.Log("(Socket) Added Arm: " + AttachedArm.name + "\nArms in range: " + partDetector.ArmsInRange.Count);
        }
        AttachedArm = arm;
        partDetector.removeArm(AttachedArm);
        //Debug.Log("(Socket) Removed Arm: " + AttachedArm.name + "\nArms in range: " + partDetector.ArmsInRange.Count);

        AttachedArm.equipArm(transform);

        isEquipped = true;

        //gameManager.CheckEquipStatus();
    }

    public void DropArm()
    {
        AttachedArm.dropArm();
        AttachedArm = null;
        isEquipped = false;

        gameManager.EnableUnequippedIndicators();
    }

    public bool IsEquipped() { return isEquipped; }
}
