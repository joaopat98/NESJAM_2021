using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBullet : Bullet
{
    private bool hitFloor = false;
    public int maxBounces = 3;
    private int bounces = 0;

    public Transform head;
    public float rayDistance = 0.1f;

    public LayerMask groundMask;

    protected override void FixedUpdate()
    {

        if (!hitFloor)
        {
            rigidbody.MovePosition(rigidbody.position + (direction.normalized + Vector2.down) * Speed * Time.deltaTime);
        }
        else
        {
            bool horizontal = direction.x == 0;

            RaycastHit2D down = Physics2D.Raycast(head.position, -transform.up, rayDistance);
            RaycastHit2D forward = Physics2D.Raycast(head.position, transform.right, rayDistance); ;

            //escolher direção do down
            if (down.collider == null && forward.collider == null)
            {
                transform.Rotate(Vector3.forward, -90);
                if (horizontal)
                {
                    direction.x = 0;
                    direction.y = transform.up.y > 0 ? -1 : 1;
                }
                else
                {
                    direction.x = transform.up.x > 0 ? -1 : 1;
                    direction.y = 0;
                }
                bounces++;
            }
            //escolher direção oposta ao down
            else if (down.collider != null && forward.collider != null)
            {
                transform.Rotate(Vector3.forward, 90);
                if (horizontal)
                {
                    direction.x = 0;
                    direction.y = transform.up.y > 0 ? 1 : -1;
                }
                else
                {
                    direction.x = transform.up.x < 0 ? -1 : 1;
                    direction.y = 0;
                }
                bounces++;

            }
            if (bounces >= maxBounces)
            {
                Destroy(this);
            }

            rigidbody.MovePosition(rigidbody.position + (direction.normalized) * Speed * Time.deltaTime);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
        if (groundMask.HasLayer(collider.gameObject.layer))
        {
            hitFloor = true;

            Vector2 normal = (collider.ClosestPoint(rigidbody.position) - rigidbody.position).normalized;
            bool wall = Vector2.Angle(normal, Vector2.up) > 80; 

            //se for parede
            if (wall)
            {
                if (direction.x > 0)
                {
                    transform.Rotate(Vector3.forward, 90);
                }
                else
                {
                    transform.Rotate(Vector3.forward, -90);
                }
                direction = new Vector2(0, 1);

            }
            else
            {
                direction = new Vector2(1, 0);
            }
        }
    }
}