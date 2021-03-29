using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusicManager : MonoBehaviour
{
    private AudioManager audioManager;

    public string intro = "MenuIntro";
    public string loop = "MenuLoop";

    void Start()
    {
        audioManager = AudioManager.instance;
        audioManager.SetMusic(intro, loop);
    }
}
