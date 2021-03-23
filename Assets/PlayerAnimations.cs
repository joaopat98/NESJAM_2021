using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    PlayerMovement movement;
    Animator anim;
    new SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void LateUpdate()
    {
        if (movement.HorizontalOutput != 0)
        {
            anim.Play("Player Run");
            renderer.flipX = movement.HorizontalOutput < 0;
        }
        else
        {
            anim.Play("Player Idle Umbrella");
        }
    }
}
