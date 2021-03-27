using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : Platform
{

    [SerializeField] private Transform[] pointsToPassThrough;
    [SerializeField] private float velocity;
    [SerializeField] private bool loop;

    private int nextPoint = 0;
    private int forward = 1;

    private Vector3 startingPos;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        startingPos = transform.position;
        transform.position = pointsToPassThrough[nextPoint].position;
    }

    private int GetNextPoint()
    {
        if (loop) { return GetNextPointLoop(); }
        else { return GetNextPointBackAndForth(); }
    }

    private int GetNextPointLoop()
    {
        if (++nextPoint == pointsToPassThrough.Length) { nextPoint = 0; }
        return nextPoint;
    }

    private int GetNextPointBackAndForth()
    {
        if (nextPoint + forward == pointsToPassThrough.Length || nextPoint + forward == -1)
        {
            forward = -forward;
        }

        nextPoint += forward;
        return nextPoint;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float step = velocity * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, pointsToPassThrough[nextPoint].position, step);
        if (Vector2.Distance(transform.position, pointsToPassThrough[nextPoint].position) < 0.01f)
        {
            GetNextPoint();
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (Vector2.Angle(Vector2.up, GetCollisionNormal(collision)) < 10f)
            {
                base.Activate();
                collision.transform.parent = transform;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            base.Deactivate();
            collision.transform.parent = null;
        }
    }
}
