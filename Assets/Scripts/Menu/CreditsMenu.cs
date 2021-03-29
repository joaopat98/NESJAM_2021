using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour
{
    public void Submit(BaseEventData data)
    {
        SceneManager.LoadScene(0);
    }
}
