using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class KeyPickup2 : MonoBehaviour
{
    public GameObject trapsToDestroy;

    [Tooltip("Distance at which the object is considered 'Near'")]
    public float nearDistance = 20f;
    public TextMeshProUGUI keyCountText;

    AN_HeroInteractive hero;


    private void Start()
    {
        hero = FindObjectOfType<AN_HeroInteractive>(); // key will get up and it will saved in "inventory"
    }


    void Update()
    {
        if (NearView() && Input.GetKeyDown(KeyCode.E))
        {
            DestroyTraps();
            hero.RedKeyCount++;
            UpdateKeyCounterUI();
            Destroy(gameObject); // Remove the key GameObject.
            SoundManagerScript.PlaySound("win");
            SoundManagerScript.PlaySound("takeKey");

        }
      

    }
    private void DestroyTraps()
    {
        if (trapsToDestroy != null)
        {
            Destroy(trapsToDestroy); // Destroy the wall GameObject.
        }
        else
        {
            Debug.LogError("traps GameObject reference is missing.");
        }
    }
    bool NearView() // it is true if you near interactive object
    {
        float distance = Vector3.Distance(transform.position, hero.transform.position);
        if (distance < nearDistance)
            return true;
        else
            return false;
    }
    void UpdateKeyCounterUI()
    {
        keyCountText.text = "Keys: " + hero.RedKeyCount;
    }
}

