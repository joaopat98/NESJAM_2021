using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : Platform
{
    [SerializeField] private float maxJumpHeight;
    [SerializeField] private float minJumpHeight;

    private float jumpHeight;

    private PlayerEntity currentPlayerOn;

    protected override void Start()
    {
        base.Start();
        jumpHeight = minJumpHeight;
    }

    void Update()
    {
        if (currentPlayerOn != null)
        {
            if (currentPlayerOn.movement.IsJumping)
            {
                jumpHeight = maxJumpHeight;
            }
            else
            {
                jumpHeight = minJumpHeight;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (Vector2.Angle(Vector2.up, GetCollisionNormal(collision)) < 10f)
            {
                base.Activate();
                currentPlayerOn = collision.collider.GetComponent<PlayerEntity>();
                currentPlayerOn.movement.ForcedJump(jumpHeight);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            base.Deactivate();
        }
    }



    Vector2 GetCollisionNormal(Collision2D collision)
    {
        Vector2 normal = Vector3.zero;
        for (int i = 0; i < collision.contactCount; i++)
        {
            normal -= collision.GetContact(i).normal;
        }
        normal /= collision.contactCount;

        return normal;
    }
}
