using UnityEngine;

public class ArmInstance
{
    [SerializeField] private ArmData data;
    
    private RarityData rarityData;

    public float damage;
    public float attackSpeed;

    public ArmInstance(ArmData armData, RarityData rarityData)
    {
        data = armData;
        this.rarityData = rarityData;
        Debug.Log("Rarity Selected: " + rarityData.rarityName);

        damage = data.baseDamage * rarityData.damageMultiplier;
        attackSpeed = data.baseAttackSpeed;
    }
}
