using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/SpellData")]
public class SpellDataOS : ScriptableObject {

    public string spellName;
    public string description;
    public int manaCost;
    public float castingTime;
    public float cooldown;
    public GameObject spellPrefab;
    public SpellType spellType; // New property to specify the spell type
    public int healAmount; // Property for healing spells
    public float freezeDuration; // Property for freezing spells
    // Add other properties as needed
}

public enum SpellType
{
    Heal,
    Freeze
}