using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    [SerializeField] float reach = 2.5f;
    [SerializeField] LayerMask interactableItems;
    Interactable currentItem;
    Transform itemPos;
    Transform cam;
    private void Start()
    {
        cam = Camera.main.transform;
        itemPos = transform.Find("Item Position");
    }
    void Update()
    {
        if (Input.GetButtonDown("Interact") || Input.GetButtonDown("Fire1"))
        {
            if (currentItem != null)
            {
                currentItem.Use();
            } else
            {
                Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, reach, interactableItems);
                if (hitInfo.collider)
                {
                    Interactable coll = hitInfo.collider.GetComponent<Interactable>();
                    if (coll.pickable)
                    {
                        currentItem = coll;
                        hitInfo.transform.position = itemPos.position;
                        hitInfo.transform.localRotation = itemPos.rotation;
                        hitInfo.transform.parent = itemPos;
                        hitInfo.transform.GetComponent<Rigidbody>().isKinematic = true;
                    }
                    else coll.Use();
                }
            }
        }
    }
    public void DropItem(InputAction.CallbackContext context)
    {
        if (context.performed && currentItem != null)
        {
            ItemUnlock();
        }
    }
    public void ItemUnlock()
    {
        currentItem.transform.parent = null;
        currentItem.GetComponent<Rigidbody>().isKinematic = false;
        currentItem = null;
    }
}
