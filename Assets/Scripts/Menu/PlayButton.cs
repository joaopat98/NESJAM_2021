using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayButton : MenuButton
{
    public override void Activate()
    {
        base.Activate();
    }
    public override void Press()
    {
        base.Press();
        //Loads next Scene 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
