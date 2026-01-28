using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Material LightBlue_Transparent;
    [SerializeField] private Material LightBlue;

    private List<Arm_Base> AllArmsList;
    [SerializeField] private List<ArmSocketScript> ArmSockets;

    private void Awake()
    {
        AllArmsList = new List<Arm_Base>();
        ArmSockets = new List<ArmSocketScript>();
    }

    public Material GetMaterial(string materialName)
    {
        switch (materialName)
        {
            case ("LightBlue_Transparent"):
                return LightBlue_Transparent;
            case ("LightBlue"):
                return LightBlue;
            default:
                return null;
        }
    }

    public void CheckEquipStatus()
    {
        bool bothEquipped = true;

        foreach (ArmSocketScript armSocket in ArmSockets)
        {
            if (!armSocket.IsEquipped())
            {
                bothEquipped = false;
            }
        }

        if (bothEquipped)
        {
            DisableIndicators();
        }
    }

    public void EnableUnequippedIndicators()
    {
        foreach(Arm_Base arm in AllArmsList)
        {
            if(!arm.IsEquipped())
            {
                arm.EnableIndicator();
            }
        }
    }

    public void DisableIndicators()
    {
        foreach(Arm_Base arm in AllArmsList)
        {
            arm.DisableIndicator();
        }
    }

    public void RegisterArm(Arm_Base arm)
    {
        AllArmsList.Add(arm);
    }

    public void RegisterSocket(ArmSocketScript socket)
    {
        ArmSockets.Add(socket);
    }
}
