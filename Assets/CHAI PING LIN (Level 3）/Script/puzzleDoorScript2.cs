using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleDoorScript2 : MonoBehaviour
{
    // Dictionary to store boolean values associated with keys.
    private Dictionary<string, bool> boolDictionary = new Dictionary<string, bool>();

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the dictionary with default values.
        boolDictionary.Add("lockRed", false);
        boolDictionary.Add("lockBlue", false);
        boolDictionary.Add("lockGreen", false);
        boolDictionary.Add("lockYellow", false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if all boolean values in the dictionary are true.
        bool allBoolsTrue = true;
        foreach (var kvp in boolDictionary)
        {
            if (!kvp.Value)
            {
                allBoolsTrue = false;
                break; // Exit the loop as soon as a false value is found.
            }
        }

        // If all boolean values are true, destroy the game object.
        if (allBoolsTrue)
        {
            Destroy(gameObject);
        }
    }

    // Set the boolean value associated with a specific key in the dictionary.
    public void SetBool(string key, bool value)
    {
        if (boolDictionary.ContainsKey(key))
        {
            boolDictionary[key] = value;
        }
        else
        {
            // Display an error message if the key is not found in the dictionary.
            Debug.LogError("Key not found in boolDictionary: " + key);
        }
    }

    // Get the boolean value associated with a specific key from the dictionary.
    public bool GetBool(string key)
    {
        if (boolDictionary.ContainsKey(key))
        {
            return boolDictionary[key];
        }
        else
        {
            // Display an error message if the key is not found in the dictionary and return false.
            Debug.LogError("Key not found in boolDictionary: " + key);
            return false;
        }
    }
}
