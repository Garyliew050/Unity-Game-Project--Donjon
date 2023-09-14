using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleDoorScript2 : MonoBehaviour
{
    private Dictionary<string, bool> boolDictionary = new Dictionary<string, bool>();

    // Start is called before the first frame update
    void Start()
    {
        boolDictionary.Add("lockRed", false);
        boolDictionary.Add("lockBlue", false);
        boolDictionary.Add("lockGreen", false);
        boolDictionary.Add("lockYellow", false);
    }

    // Update is called once per frame
    void Update()
    {
        bool allBoolsTrue = true;
        foreach (var kvp in boolDictionary)
        {
            if (!kvp.Value)
            {
                allBoolsTrue = false;
                break; 
            }
        }

        if (allBoolsTrue)
        {
            Destroy(gameObject);
        }
    }

    public void SetBool(string key, bool value)
    {
        if (boolDictionary.ContainsKey(key))
        {
            boolDictionary[key] = value;
        }
        else
        {
            Debug.LogError("Key not found in boolDictionary: " + key);
        }
    }

    public bool GetBool(string key)
    {
        if (boolDictionary.ContainsKey(key))
        {
            return boolDictionary[key];
        }
        else
        {
            Debug.LogError("Key not found in boolDictionary: " + key);
            return false; 
        }
    }
}
