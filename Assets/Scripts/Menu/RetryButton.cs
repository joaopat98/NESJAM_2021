using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RetryButton : MenuButton
{
    public override void Activate()
    {
        base.Activate();
    }
    public override void Press()
    {
        base.Press();
        //Loads first scene (main menu)
        SceneManager.LoadScene(0);
    }
}
