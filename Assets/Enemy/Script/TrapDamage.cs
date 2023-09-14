using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    public int trapDamage;
    public float cooldowntime = 2f;

    protected void OnTriggerEnter(Collider collision)
    {
        collision.GetComponent<PlayerController>().TakeDamage(trapDamage);
        SoundManagerScript.PlaySound("hit");
           
    }

}
