using UnityEngine;

public class ThrowableBomb : EnemyBullet
{
    [SerializeField] LayerMask BounceMask;
    /*[SerializeField]*/ int NumBounces = 1;
    [SerializeField] float MaxBounceHeight;
    [SerializeField] float GravityScale = 1;
    int currentBounce = 0;
    float VerticalVelocity;
    float HorizontalVelocity;
    int HorizontalDirection;

    public override void Init(Vector2 direction)
    {
        HorizontalVelocity = direction.x;
        //HorizontalDirection = Mathf.RoundToInt(Mathf.Sign(direction.x));
        Bounce();
    }

    void Bounce()
    {
        if (currentBounce < NumBounces)
        {
            VerticalVelocity = Mathf.Sqrt(-(MaxBounceHeight * ((NumBounces - currentBounce) / (float)NumBounces)) * 2 * Physics.gravity.y * GravityScale);
            Debug.Log(currentBounce);
            currentBounce++;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /*
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
        if (BounceMask.HasLayer(collider.gameObject.layer))
        {
            Vector2 hitPoint = collider.ClosestPoint(rigidbody.position);
            if (Vector2.Angle(rigidbody.position - hitPoint, Vector2.up) > 80)
            {
                Destroy(gameObject);
            }
            else
            {
                Bounce();
            }
        }
    }
    */

    protected override void FixedUpdate()
    {
        VerticalVelocity += Physics2D.gravity.y * GravityScale * Time.fixedDeltaTime;
        rigidbody.velocity = new Vector2(HorizontalVelocity, VerticalVelocity);
    }
}