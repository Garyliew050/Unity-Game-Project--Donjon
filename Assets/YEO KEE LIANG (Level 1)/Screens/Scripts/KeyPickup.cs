using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class KeyPickup : MonoBehaviour
{
    public int keyNumber; // Set this to a unique number for each key (1, 2, 3, 4).
    public WallDestroyer wallDestroyer; // Reference to the script that handles wall destruction.
    public Timer timer; // Reference to the timer script.

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
            if (keyNumber == wallDestroyer.GetNextKeyNumber())
            {
                if (keyNumber == 4)
                {
                    hero.RedKeyCount++;
                    UpdateKeyCounterUI();
                    wallDestroyer.KeyCollected(keyNumber);
                    Destroy(gameObject); // Remove the key GameObject.
                    SoundManagerScript.PlaySound("win");
                }
                else
                {
                    // Key collected in the correct order.
                    wallDestroyer.KeyCollected(keyNumber);
                    Destroy(gameObject); // Remove the key GameObject.
                    SoundManagerScript.PlaySound("takeKey");
                }
            }
            else
            {
                timer.DecreaseTime(20.0f);
                SoundManagerScript.PlaySound("lose");
            }

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

