using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    float inputMovement;
    bool inputJump;
    [SerializeField] float speed;
    CharacterController2D controller;
    // Start is called before the first frame update
    void Start()
    {
        //it owrked :)
        controller = GetComponent<CharacterController2D>();
    }


    void FixedUpdate()
    {
        controller.Move(speed * inputMovement, false, inputJump);
    }

    public void OnMove(InputValue value)
    {
        inputMovement = value.Get<Vector2>().x;
    }

    public void OnJump(InputValue value)
    {
        inputJump = value.Get<int>() == 1;
        Debug.Log(inputJump);
    }
}
