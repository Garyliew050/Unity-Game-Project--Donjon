using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    //Version2
    private GameObject currentSpell;
    [SerializeField]
    private SpellDataOS castingspell;
    public Transform Player;
    private List<GameObject> enemiesToFreeze = new List<GameObject>();

    public AudioClip spellPickupSound;
    //public AudioSource spellPickupSoundSource;
    

    //Version 2
    public void CastSpell(SpellDataOS spellData)
    {

        //Version 2
        // player.clip = pickupsound;
        // // Play();
        // //                 player.clip = walk;
        

        Debug.Log("Attempting to find spell with SpellType: " + spellData);
        castingspell = spellData;
        if(currentSpell != null){
            Destroy(currentSpell);
        }
        currentSpell = Instantiate(spellData.spellPrefab);
            switch (spellData.spellType)
            {
                case SpellType.Heal:
                    // Cast a healing spell
                    AudioSource.PlayClipAtPoint(spellPickupSound, transform.position);
                    HealPlayer(spellData.healAmount);
                    break;
                case SpellType.Freeze:
                    // Cast a freezing spell
                    AudioSource.PlayClipAtPoint(spellPickupSound, transform.position);
                    FreezeEnemies(spellData.freezeDuration);
                    break;
                // Add cases for other spell types as needed
            }
    }



    private void HealPlayer(int healAmount)
    {
        // Implement healing logic here
        Player.GetComponent<PlayerController>().Healing(healAmount);
        Debug.Log("Heal");
    }

    private void FreezeEnemies(float duration)
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        // Add the detected enemies to the list
        foreach (GameObject enemy in enemyObjects)
        {
            enemiesToFreeze.Add(enemy);
        }
        // // Implement freezing enemies logic here
        foreach (var enemy in enemiesToFreeze) // enemiesToFreeze is a list of enemy GameObjects to freeze
        {
            Debug.Log("Found enemy: " + enemy.name);
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.Freeze();
                StartCoroutine(UnfreezeAfterDuration(enemyController, duration));
            }
        }
    }

    private IEnumerator UnfreezeAfterDuration(EnemyController enemyController, float duration)
    {
        yield return new WaitForSeconds(duration);

        // Unfreeze the enemy after the specified duration
        enemyController.Unfreeze();
    }


}
