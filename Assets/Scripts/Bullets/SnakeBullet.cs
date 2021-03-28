using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBullet : PlayerBullet
{
    private bool hitFloor = false;
    public int maxBounces = 3;
    private int bounces = 0;

    public Transform head;
    public Transform WallFinder;
    public float rayDistance = 0.1f;

    public LayerMask groundMask;
    float colliderSize;

    void Start()
    {
        colliderSize = collider.bounds.extents.y;
    }

    protected override void Update()
    {
        base.Update();
        if (!hitFloor)
        {
            transform.localPosition += (Vector3)(direction.normalized + Vector2.down) * Speed * Time.deltaTime;
        }
        else
        {
            Vector3 moveVector;
            if (transform.parent != null)
            {
                moveVector = transform.parent.InverseTransformVector(head.right * Speed * Time.deltaTime);
            }
            else
            {
                moveVector = head.right * Speed * Time.deltaTime;
            }
            transform.localPosition += moveVector;
            bool horizontal = direction.x != 0;

            RaycastHit2D down = Physics2D.Raycast(head.position, -head.up, rayDistance, groundMask);
            RaycastHit2D forward = Physics2D.Raycast(head.position, head.right, rayDistance, groundMask);

            //escolher direção do down
            if (down.collider == null && forward.collider == null)
            {
                RaycastHit2D wallHit = Physics2D.Raycast(WallFinder.position, -WallFinder.right, rayDistance, groundMask);
                transform.Rotate(Vector3.forward, -90 * Mathf.Sign(direction.x));
                transform.position = wallHit.point + wallHit.normal * colliderSize;
                bounces++;
            }
            //escolher direção oposta ao down
            else if (down.collider != null && forward.collider != null)
            {
                transform.Rotate(Vector3.forward, 90 * Mathf.Sign(direction.x));
                bounces++;
                transform.position = forward.point + forward.normal * colliderSize;
            }
            if (bounces > maxBounces)
            {
                Destroy(gameObject);
            }

            //Debug.Log(bounces);
        }
    }

    protected override void FixedUpdate()
    {
        /*
        if (!hitFloor)
        {
            rigidbody.MovePosition(rigidbody.position + (direction.normalized + Vector2.down) * Speed * Time.deltaTime);
        }
        else
        {
            rigidbody.MovePosition(rigidbody.position + (Vector2)head.right * Speed * Time.deltaTime);
            bool horizontal = direction.x != 0;

            RaycastHit2D down = Physics2D.Raycast(head.position, -head.up, rayDistance, groundMask);
            RaycastHit2D forward = Physics2D.Raycast(head.position, head.right, rayDistance, groundMask);

            //escolher direção do down
            if (down.collider == null && forward.collider == null)
            {
                RaycastHit2D wallHit = Physics2D.Raycast(WallFinder.position, -WallFinder.right, rayDistance, groundMask);
                transform.Rotate(Vector3.forward, -90 * Mathf.Sign(direction.x));
                rigidbody.MovePosition(wallHit.point + wallHit.normal * colliderSize);
                bounces++;
            }
            //escolher direção oposta ao down
            else if (down.collider != null && forward.collider != null)
            {
                transform.Rotate(Vector3.forward, 90 * Mathf.Sign(direction.x));
                bounces++;
                rigidbody.MovePosition(forward.point + forward.normal * colliderSize);
            }
            if (bounces > maxBounces)
            {
                Destroy(gameObject);
            }

            //Debug.Log(bounces);
    
        }
        */
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
        if (!hitFloor && groundMask.HasLayer(collider.gameObject.layer))
        {
            hitFloor = true;
            Vector2 closestPoint = collider.ClosestPoint(rigidbody.position);
            Vector2 normal = (rigidbody.position - closestPoint).normalized;
            RaycastHit2D hit;
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
            }
            direction = new Vector2(Mathf.Sign(direction.x), 0);
            hit = Physics2D.Raycast(head.position, -transform.up, rayDistance, groundMask);
            transform.position = hit.point + hit.normal * colliderSize;
            if (collider.CompareTag("Moving Platform"))
            {
                transform.SetParent(collider.transform);
            }
        }
    }
}