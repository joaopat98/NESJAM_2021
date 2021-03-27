using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class SpriteResolverWorldSwitcher : MonoBehaviour
{
    SpriteResolver resolver;
    void Start()
    {
        resolver = GetComponent<SpriteResolver>();
        WorldManager.OnWorldSwitch.AddListener(world => resolver.SetCategoryAndLabel(world.ToString(), resolver.GetLabel()));
    }
}
