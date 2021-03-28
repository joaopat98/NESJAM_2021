using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MenuButton
{
    public override void Activate()
    {
        base.Activate();
    }
    public override void Press()
    {
        base.Press();
        Application.Quit();
    }
}
