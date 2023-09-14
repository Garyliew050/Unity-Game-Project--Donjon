using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class AN_DoorKey : MonoBehaviour
{
    [Tooltip("True - red key object, false - blue key")]
    public bool isRedKey = true;

    [Tooltip("Distance at which the object is considered 'Near'")]
    public float nearDistance = 50f;
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
            if (isRedKey)
            {
                hero.RedKeyCount++;
                UpdateKeyCounterUI();
            }

            else
            {
                hero.BlueKey = true;
            }
            Destroy(gameObject);
            SoundManagerScript.PlaySound("takeKey");
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
        keyCountText.text = "Red Keys: " + hero.RedKeyCount;
    }
}
