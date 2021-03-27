using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberBomb : MonoBehaviour
{
    [SerializeField] LayerMask ExplodeMask;
    new SpriteRenderer renderer;
    bool exploded;
    public bool Flip;
    public BomberDrop dropper;
    [SerializeField] GameObject ExplosionPrefab;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!exploded && ExplodeMask.HasLayer(collider.gameObject.layer))
        {
            exploded = true;
            StartCoroutine(Explode());
        }
    }

    void Update()
    {
        renderer.flipX = Flip;
    }

    IEnumerator Explode()
    {
        yield return 0;
        yield return 0;
        dropper.Regen();
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
