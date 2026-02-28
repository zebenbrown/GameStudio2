using UnityEngine;

public class RangeIndicatorReadjustment : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject armParent;

    private Vector3 positionDisplacement;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        GetComponent<MeshRenderer>().material = gameManager.GetMaterial("LightBlue_Transparent");

        armParent = GetComponentInParent<Arm_Base>().gameObject;

        positionDisplacement = new Vector3(0.0f, 1.0f, 0.0f);
    }

    private void Update()
    {
        ResetPosition();
    }

    private void ResetPosition()
    {
        transform.SetPositionAndRotation(armParent.transform.position + positionDisplacement, Quaternion.identity);
    }
}
