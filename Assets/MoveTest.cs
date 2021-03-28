using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    [SerializeField] float amplitude;
    [SerializeField] float frequency;
    [SerializeField] Vector3 dir;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPos + dir.normalized * amplitude * Mathf.Sin(Time.time * frequency);
    }
}
