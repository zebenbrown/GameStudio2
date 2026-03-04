using UnityEngine;

[CreateAssetMenu(fileName = "RarityData", menuName = "Scriptable Objects/RarityData")]
public class RarityData : ScriptableObject
{
    public string rarityName;
    public Color rarityColor;
    public float damageMultiplier;
    public float dropChance;
}
