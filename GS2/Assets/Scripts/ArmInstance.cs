using UnityEngine;

public class ArmInstance
{
    [SerializeField] private ArmData data;

    public float damage;
    public float attackSpeed;

    public ArmInstance(ArmData armData)
    {
        data = armData;

        damage = data.baseDamage;
        attackSpeed = data.baseAttackSpeed;
    }
}
