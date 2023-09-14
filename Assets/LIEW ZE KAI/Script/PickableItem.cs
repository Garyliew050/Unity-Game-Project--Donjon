using UnityEngine;

public class PickableItem : MonoBehaviour
{
    private void Start()
    {
        // Make sure the tag is set to "Pickable" in the Unity Inspector for pickable items.
        gameObject.tag = "Pickable";
    }
}
