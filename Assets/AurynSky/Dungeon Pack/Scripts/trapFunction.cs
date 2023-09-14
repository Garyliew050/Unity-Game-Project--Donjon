using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapFunction : MonoBehaviour
{
    public int damageAmount = 10; // 陷阱造成的伤害

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 检查是否与玩家碰撞
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damageAmount); // 调用玩家的 TakeDamage 函数来扣除血量
                SoundManagerScript.PlaySound("hit");
            }
        }
    }
}
