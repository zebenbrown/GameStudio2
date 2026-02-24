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

    private void Start()
    {
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
}
