using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThrowableBomb : EnemyBullet
{
    [SerializeField] LayerMask BounceMask;
    /*[SerializeField]*/
    int NumBounces = 1;
    [SerializeField] float MaxBounceHeight;
    [SerializeField] float GravityScale = 1;
    int currentBounce = 0;
    float VerticalVelocity;
    float HorizontalVelocity;
    int HorizontalDirection;

    public override void Init(Vector2 direction)
    {
        HorizontalVelocity = direction.x;
        VerticalVelocity = Mathf.Sqrt(-(MaxBounceHeight * ((NumBounces - currentBounce) / (float)NumBounces)) * 2 * Physics.gravity.y * GravityScale);
    }

    protected override void FixedUpdate()
    {
        VerticalVelocity += Physics2D.gravity.y * GravityScale * Time.fixedDeltaTime;
        rigidbody.velocity = new Vector2(HorizontalVelocity, VerticalVelocity);
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (HitMask.HasLayer(collider.gameObject.layer))
        {
            Hit(collider.gameObject);
        }
        if (DestroyMask.HasLayer(collider.gameObject.layer))
        {
            StartCoroutine(DestroyNextFrame());
        }
    }

    protected virtual void Explode() { }

    IEnumerator DestroyNextFrame()
    {
        yield return new WaitForFixedUpdate();
        Explode();
        Destroy(gameObject);
    }
}