using UnityEditor;
using UnityEngine;
// Youtube: Sunny Valey Studio


public class PlayerPickup : MonoBehaviour
{
    [SerializeField]
    private LayerMask pickableLayerMask;

    [SerializeField]
    private Transform playerCameraTransform;

    [SerializeField]
    private GameObject pickUpUI;

    [SerializeField]
    [Min(1)]
    private float hitRange = 3;

    [SerializeField]
    private Transform pickUpParent;

    [SerializeField] 
    private GameObject inHandItem;

    [SerializeField]
    private KeyCode interactKey = KeyCode.Z;

    private RaycastHit hit;

    private void Update()
    {

        if (hit.collider != null)
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
            pickUpUI.SetActive(false);
        }
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, hitRange, pickableLayerMask))
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
            pickUpUI.SetActive(true);
        }
        if (Input.GetKeyDown(interactKey))
        {
            if(hit.collider != null)
            {
                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = false;
                    rb.isKinematic = false;
                }
                Collider collider = rb.GetComponent<Collider>();
                collider.enabled = false;
                inHandItem = hit.collider.gameObject;
                inHandItem.transform.position = new Vector3(0.0f, -0.2f, 0.5f);
                inHandItem.transform.rotation = Quaternion.Euler(0, 180, 0);
                inHandItem.transform.SetParent(pickUpParent.transform, false);
                
            }
        }
    }
}
