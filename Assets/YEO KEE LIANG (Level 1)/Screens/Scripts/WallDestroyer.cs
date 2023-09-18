using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class WallDestroyer : MonoBehaviour
{
    public GameObject wallToDestroy; // Reference to the wall GameObject.

    // Define the first key that needs to be collected.
    public int initialExpectedKey = 1;

    private int collectedKeys = 0;
    private int expectedKey;

    private void Start()
    {
        // Initialize the expected key to the initialExpectedKey value.
        expectedKey = initialExpectedKey;
    }

    public void KeyCollected(int keyNumber)
    {
        if (keyNumber == expectedKey)
        {
            collectedKeys++;
            expectedKey++;

            if (collectedKeys == 4)
            {
                DestroyWall();
            }
        }
    }

    public int GetNextKeyNumber()
    {
        return expectedKey;
    }

    private void DestroyWall()
    {
        if (wallToDestroy != null)
        {
            Destroy(wallToDestroy); // Destroy the wall GameObject.
        }
        else
        {
            Debug.LogError("Wall GameObject reference is missing.");
        }
    }
}


