using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateChildren : MonoBehaviour
{
    [SerializeField] Transform Children;
    new SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Children.rotation = Quaternion.Euler(0, renderer.flipX ? 180 : 0, 0);
    }
}