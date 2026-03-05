using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Arm_Base : MonoBehaviour
{
    protected GameManager gameManager;

    protected Rigidbody rb;
    protected new Collider collider;
    protected Quaternion startRotation;
    [SerializeField] protected GameObject rangeIndicator;

    protected ArmSocketScript attachedArmSocket;

    protected bool isEquipped = false;
    public bool isEnemyArm = false;

    protected AudioSource audioSource;
    protected float activationVolume = 0.6f;
    
    [Header("Scriptable Objects")]
    protected RarityData rarityData; //gets assigned as the random rarity and needs to be copied to mutate values
    [SerializeField] protected List<RarityData> rarities; //randomize which one gets selected
    [SerializeField] private ArmData armData;
    protected ArmInstance arm;

    private void Start()
    {
        //rarities = new List<RarityData>();
        rarityData = getRandomRarity();
        arm = new ArmInstance(armData, rarityData);
        gameManager = FindAnyObjectByType<GameManager>();
        gameManager.registerArm(this);

        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning("RigidBody Not Found!!!");
        }
        collider = GetComponentInChildren<Collider>(); 
        if (collider == null)
        {
            Debug.LogWarning("Collider Not Found!!!");
        }
        startRotation = transform.rotation;

        audioSource = GetComponent<AudioSource>();

        armSpecificStart();
    }

    public void setIndiatorMat(Material mat)
    {
        rangeIndicator.GetComponent<MeshRenderer>().material = mat;
    }

    public virtual void equipArm(Transform armSocket)
    {
        transform.parent = armSocket;
        attachedArmSocket = armSocket.GetComponent<ArmSocketScript>();
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        //resetTransform();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.useGravity = false;
        collider.enabled = false;

        isEquipped = true;

        disableIndicator();

        if (attachedArmSocket.gameObject.name == "Arm_Socket_R")
        {
            transform.localRotation = new Quaternion(0.0f, 0.0f, 180.0f, 1.0f);
        }
        else
        {
            transform.localRotation = Quaternion.identity;
        }

        specificEquip();
    }

    protected abstract void specificEquip();


    public virtual void dropArm()
    {
        transform.parent = null;
        attachedArmSocket = null;
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;
        collider.enabled = true;

        isEquipped = false;

        enableIndicator();

        specificDrop();
    }

    protected abstract void specificDrop();

    public void disableIndicator()
    {
        rangeIndicator.gameObject.SetActive(false);
    }
    public void enableIndicator()
    {
        rangeIndicator.gameObject.SetActive(true);
    }


    protected void resetTransform()
    {
        //transform.SetLocalPositionAndRotation(Vector3.zero, startRotation);
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    protected abstract void playActivateSound();

    public bool IsEquipped() { return isEquipped; }

    protected abstract void armSpecificStart();

    public abstract void armMainAction();

    protected RarityData getRandomRarity()
    {
        //common
        var commonData = rarities[0];
        
        //uncommon
        var uncommonData = rarities[1];
        
        //rare
        var rareData = rarities[2];
        
        //epic
        var epicData = rarities[3];
        
        //legendary
        var legendaryData = rarities[4];

        
        //0 - 35%
        //1 - 30%
        //2 - 20%
        //3 - 10%
        //4 - 5%
        var commonChance = commonData.dropChance;
        var uncommonChance = uncommonData.dropChance;
        var rareChance = rareData.dropChance;
        var epicChance = epicData.dropChance;
        var legendaryChance = legendaryData.dropChance;

        //float rarity = Random.Range(0.0f, 4.0f);
        //4.0f minus rarity return value times 

        float randomValue = Random.value;
        //if random value is between 0.0 and 0.1
        if (randomValue <= legendaryChance)
        {
            return rarities[4];
        }
        
        //if random value is between 0.11 and 0.25
        else if (randomValue <= epicChance)
        {
            return rarities[3];
        }
        
        //if random value is between 0.26 and 0.35
        else if (randomValue <= rareChance)
        {
            return rarities[2];
        }
        
        //if random value is between 0.36 and 0.55
        else if (randomValue <= uncommonChance)
        {
            return rarities[1];
        }
        
        //if random value is greater than 0.55
        return rarities[0];
    }
    
}
