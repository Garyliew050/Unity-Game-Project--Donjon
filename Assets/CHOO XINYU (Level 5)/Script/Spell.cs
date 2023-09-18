using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    // public string spellName;
    // public string description;
    // public int manaCost;
    // public float castingTime;
    // public float cooldown;
    // public GameObject spellPrefab;
    // public SpellType spellType; // New property to specify the spell type
    // public int healAmount; // Property for healing spells
    // public float freezeDuration; // Property for freezing spells
    // //Add other properties as needed

    [SerializeField]
    private SpellDataOS spellData;

    private void OnTriggerEnter(Collider other){
        SpellManager spellManager = other.GetComponent<SpellManager>();
        if(other.CompareTag("Player")){
            spellManager.CastSpell(spellData);  
            Destroy(gameObject);
        }
    }

    // // public static Spell DefaultSpell
    // // {
    // //     get
    // //     {
    // //         return new Spell
    // //         {
    // //             spellName = "Default",
    // //             spellType = SpellType.None, // Set the default spell type here
    // //             manaCost = 0,
    // //             cooldown = 0,
    // //             // Set other default values here
    // //         };
    // //     }
    // // }


}

// public enum SpellType
// {
//     None,
//     Heal,
//     Freeze
// }

// Spell fireballSpell = new Spell
// {
//     spellName = "Fireball",
//     spellType = SpellType.Damage,
//     manaCost = 10,
//     cooldown = 3f,
//     spellPrefab = fireballPrefab // Assign the fireball prefab
// };

// Spell healingSpell = new Spell
// {
//     spellName = "Healing Touch",
//     spellType = SpellType.Heal,
//     manaCost = 5,
//     cooldown = 2f,
//     spellPrefab = healingEffectPrefab, // Assign the healing effect prefab
//     healAmount = 30
// };



// [CreateAssetMenu(filename = "Spell", menuName = "Spell")]
// public class SpellDataOS : ScriptableObject {

//     public string spellName;
//     public string description;
//     public int manaCost;
//     public float castingTime;
//     public float cooldown;
//     public GameObject spellPrefab;
//     public SpellType spellType; // New property to specify the spell type
//     public int healAmount; // Property for healing spells
//     public float freezeDuration; // Property for freezing spells
//     // Add other properties as needed
// }