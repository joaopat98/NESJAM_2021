using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTest : Platform
{
    Vector3 lastPos;
    [SerializeField] float frequency = 1;
    [SerializeField] float amplitude = 1;
    Vector3 startPos;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPos + Mathf.Sin(Time.time * Mathf.PI * frequency) * Vector3.right * amplitude;
        if (active)
            controller.Move(transform.position - lastPos);
        lastPos = transform.position;
    }
}
