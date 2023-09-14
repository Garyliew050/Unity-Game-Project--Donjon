using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioSource levelMusic; // Assign the AudioSource component from the inspector
    public AudioSource deathMusic; // Assign the AudioSource component from the inspector

    public bool levelSong = true;
    public bool deathSong = false;

    public void LevelMusic()
    {
        levelSong = true;
        deathSong = false;
        levelMusic.Play();
    }

    public void DeathSound()
    {
        if (levelMusic.isPlaying)
        {
            levelMusic.Stop();
        }
        if (!deathMusic.isPlaying && !deathSong)
        {
            deathMusic.Play();
            deathSong = true;
        }
    }
}
