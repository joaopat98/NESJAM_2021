using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTest : Platform
{

    [SerializeField] private Transform[] pointsToPassThrough;
    [SerializeField] private float velocity;
    
    private int nextPoint;
    private bool forward;

    private Vector3 startingPos;

    // Start is called before the first frame update
    void Start(){
        startingPos = transform.position;
        nextPoint = 0;
        transform.position = pointsToPassThrough[nextPoint].position;
        forward = true;
        GetNextPoint();
    }

    private int GetNextPoint(){
        if (forward){
            //Go forward
            if (nextPoint + 1 == pointsToPassThrough.Length){
                nextPoint -= 1;
                forward = false;
            } else{ nextPoint += 1; }
        } else {
            //Go back
            if (nextPoint - 1 == -1 ){
                nextPoint += 1;
                forward = true;
            } else { nextPoint -= 1; }
        }

        return nextPoint;
    }

    // Update is called once per frame
    void FixedUpdate(){
        float step = velocity * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, pointsToPassThrough[nextPoint].position, step);
        if (Vector3.Distance(transform.position, pointsToPassThrough[nextPoint].position) < 0.01f){
            GetNextPoint();
        }
    }

    /* Vector3 lastPos;
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
        transform.position = startPos + Mathf.Sin(Time.time * Mathf.PI * frequency) * direction.normalized * amplitude;
        if (active)
        {
            //controller.Move(rigidbody.position - lastPos);
        }
        lastPos = transform.position;
    } */
}
