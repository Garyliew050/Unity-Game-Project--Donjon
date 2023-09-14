using UnityEngine;

public class ChestInteraction : MonoBehaviour
{
    private Animator chestAnimator;
    private bool isChestOpen = false;

    private void Start()
    {
        chestAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check for player input (E key)
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Toggle the chest open/close state
            isChestOpen = !isChestOpen;

            // Trigger the chest animation
            chestAnimator.SetBool("IsOpen", isChestOpen);

            // Optionally, play a sound effect or perform other actions here when the chest is opened.
        }
    }
}
