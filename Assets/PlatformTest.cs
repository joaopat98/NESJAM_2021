using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTest : Platform
{
    Vector3 lastPos;
    [SerializeField] float frequency = 1;
    [SerializeField] float amplitude = 1;
    [SerializeField] Vector2 direction = Vector2.right;
    new Rigidbody2D rigidbody;
    Vector2 startPos;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rigidbody = GetComponent<Rigidbody2D>();
        startPos = rigidbody.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody.MovePosition(startPos + Mathf.Sin(Time.time * Mathf.PI * frequency) * direction.normalized * amplitude);
        if (active)
            controller.Move(transform.position - lastPos);
        lastPos = transform.position;
    }
}
