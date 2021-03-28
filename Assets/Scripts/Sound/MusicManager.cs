using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioManager audioManager;
    private WorldType? worldType = null;

    void Start()
    {
        audioManager = AudioManager.instance;
    }
    void Update()
    {
        if (worldType != WorldManager.currentWorld)
        {
            worldType = WorldManager.currentWorld;
            SwitchMusic();
        }
    }

    private void SwitchMusic()
    {
        switch (worldType)
        {
            case WorldType.Light:
                audioManager.SetMusic("LightIntro", "LightLoop");
                break;
            case WorldType.Dark:
                audioManager.SetMusic("DarkIntro", "DarkLoop");
                break;
            default:
                Debug.LogWarning("No music assigned to world type " + worldType);
                break;
        }
    }
}
