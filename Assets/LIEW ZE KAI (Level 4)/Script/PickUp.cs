using UnityEngine;

public class PickUp : MonoBehaviour
{
    private GameObject pickedItem;
    private bool isPickingUp = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isPickingUp)
        {
            TryPickup();
        }
        else if (Input.GetKeyDown(KeyCode.E) && isPickingUp)
        {
            DropItem();
        }
    }

    void TryPickup()
    {
        // Perform a raycast to check if there's a pickable item in front of the character.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
        {
            if (hit.collider.CompareTag("Pickable"))
            {
                PickUpItem(hit.collider.gameObject);
            }
        }
    }

    void PickUpItem(GameObject item)
    {
        pickedItem = item;
        pickedItem.GetComponent<Collider>().enabled = false;
        pickedItem.GetComponent<Rigidbody>().isKinematic = true;
        pickedItem.transform.SetParent(transform);
        pickedItem.transform.localPosition = new Vector3(0f, 0.5f, 1f); // Adjust the position as needed.
        isPickingUp = true;
    }

    void DropItem()
    {
        if (pickedItem != null)
        {
            pickedItem.GetComponent<Collider>().enabled = true;
            pickedItem.GetComponent<Rigidbody>().isKinematic = false;
            pickedItem.transform.SetParent(null);
            pickedItem = null;
            isPickingUp = false;
        }
    }
}