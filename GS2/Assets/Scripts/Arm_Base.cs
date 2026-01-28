using Unity.VisualScripting;
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

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        gameManager.RegisterArm(this);

        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning("RigidBody Not Found!!!");
        }
        collider = GetComponent<Collider>(); 
        if (collider == null)
        {
            Debug.LogWarning("Collider Not Found!!!");
        }
        startRotation = transform.rotation;

        ArmSpecificStart();
    }

    public void SetIndiatorMat(Material mat)
    {
        rangeIndicator.GetComponent<MeshRenderer>().material = mat;
    }

    public virtual void EquipArm(Transform armSocket)
    {
        transform.parent = armSocket;
        attachedArmSocket = armSocket.GetComponent<ArmSocketScript>();
        ResetTransform();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        collider.enabled = false;

        isEquipped = true;

        DisableIndicator();

        if (attachedArmSocket.gameObject.name == "Arm_Socket_R")
        {
            transform.rotation = new Quaternion(0.0f, 0.0f, 180.0f, 1.0f);
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }

        SpecificEquip();
    }

    protected abstract void SpecificEquip();

    public void DisableIndicator()
    {
        rangeIndicator.gameObject.SetActive(false);
    }

    public virtual void DropArm()
    {
        transform.parent = null;
        attachedArmSocket = null;
        rb.constraints = RigidbodyConstraints.None;
        collider.enabled = true;

        isEquipped = false;

        EnableIndicator();

        SpecificDrop();
    }

    protected abstract void SpecificDrop();

    public void EnableIndicator()
    {
        rangeIndicator.gameObject.SetActive(true);
    }


    protected void ResetTransform()
    {
        transform.SetLocalPositionAndRotation(Vector3.zero, startRotation);
    }

    public bool IsEquipped() { return isEquipped; }

    protected abstract void ArmSpecificStart();

    public abstract void ArmMainAction();
}
