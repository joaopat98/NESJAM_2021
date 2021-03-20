using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusterBullet : MonoBehaviour
{
    Vector3 direction;
    [SerializeField] LayerMask HitMask;
    [SerializeField] LayerMask DestroyMask;
    [SerializeField] float Speed;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.gameObject);
        if (HitMask == (HitMask | (1 << collider.gameObject.layer)))
        {
            //hit target
        }
        if (DestroyMask == (DestroyMask | (1 << collider.gameObject.layer)))
        {
            Destroy(gameObject);
        }
    }

    public void Init(Vector3 direction)
    {
        this.direction = direction;
    }

    void Update()
    {
        transform.Translate(direction.normalized * Speed * Time.deltaTime, Space.World);
    }
}
