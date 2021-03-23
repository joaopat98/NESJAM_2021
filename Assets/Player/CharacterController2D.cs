using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    Vector2 speed;
    public new Rigidbody2D rigidbody { get; private set; }
    public new Collider2D collider { get; private set; }
    [Range(2, 32)]
    [SerializeField] int VerticalRays = 8;
    [Range(2, 32)]
    [SerializeField] int HorizontalRays = 8;
    [SerializeField] float SkinWidth = 0.02f;
    [SerializeField] LayerMask PlatformMask = 0;
    [Range(0, 90)]
    [SerializeField] float MaxSlope = 10;
    public bool isGrounded { get; private set; }
    Vector2 deltaAcum = Vector2.zero;
    float timeDelta = 0;
    public Vector2 velocity
    {
        get
        {
            if (timeDelta == 0f)
                return rigidbody.velocity;
            else
                return deltaAcum / timeDelta;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Bounds bounds = collider.bounds;
        Vector2 start = bounds.min;
        float width = bounds.size.x;
        Vector2 segment = Vector2.right * width / (HorizontalRays - 1);
        isGrounded = false;
        timeDelta += Time.deltaTime;
        if (velocity.y > 0)
        {
            isGrounded = false;
        }
        else
        {
            for (int i = 0; i < HorizontalRays; i++)
            {
                Vector2 rayOrigin = start + i * segment + SkinWidth * Vector2.up;
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, SkinWidth * 2, PlatformMask);
                if (hit.collider != null && Vector2.Angle(Vector3.up, hit.normal) < MaxSlope)
                {
                    isGrounded = true;
                    break;
                }
            }
        }
    }

    bool CheckRays(Vector2 origin, Vector2 end, int segments, Vector2 direction, float distance)
    {
        Vector2 segment = (end - origin) / (segments - 1);
        for (int i = 0; i < segments; i++)
        {
            Vector2 rayOrigin = origin + i * segment;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, distance, PlatformMask);
            if (hit.collider != null && Vector2.Angle(hit.normal, -direction) < 20)
            {
                return true;
            }
        }
        return false;
    }

    Vector2 FilterCollisionVelocity(Vector2 deltaMovement)
    {
        Bounds bounds = collider.bounds;
        Vector2 start = bounds.min;
        float width = bounds.size.x;
        Vector2 origin, end, direction;
        if (deltaMovement.x > 0)
        {
            origin = (Vector2)bounds.max + SkinWidth * Vector2.left;
            end = origin + bounds.size.y * Vector2.down;
            direction = Vector2.right;
        }
        else
        {
            origin = (Vector2)bounds.min + SkinWidth * Vector2.right;
            end = origin + bounds.size.y * Vector2.up;
            direction = Vector2.left;
        }
        deltaMovement.x = CheckRays(origin, end, HorizontalRays, direction, SkinWidth * 2) ? 0 : deltaMovement.x;
        if (deltaMovement.y > 0)
        {
            origin = (Vector2)bounds.max + SkinWidth * Vector2.down;
            end = origin + bounds.size.x * Vector2.left;
            direction = Vector2.up;
            deltaMovement.y = CheckRays(origin, end, VerticalRays, direction, SkinWidth * 2) ? 0 : deltaMovement.y;
        }
        return deltaMovement;
    }

    public void Move(Vector2 deltaMovement)
    {
        Vector2 before = deltaMovement;
        Vector2 after = FilterCollisionVelocity(deltaMovement);
        deltaAcum += after;
        Debug.Log($"Before: {before} After: {after}");
    }

    void FixedUpdate()
    {
        if (timeDelta > 0)
        {
            rigidbody.velocity = deltaAcum / timeDelta;
            deltaAcum = Vector2.zero;
            timeDelta = 0;
        }

    }

}
