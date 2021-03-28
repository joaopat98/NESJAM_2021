using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] int Damage = 5;
    bool exploded;
    [SerializeField] float Range;
    [SerializeField] float HorizontalImpulse = 3;
    [SerializeField] float UpwardImpulse = 5;
    [SerializeField] float DownwardImpulse = 1;

    void OnValidate()
    {
        Range = Mathf.Max(Range, 0);
        GetComponent<CircleCollider2D>().radius = Range;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!exploded && collider.CompareTag("Player"))
        {
            exploded = true;
            PlayerEntity player = PlayerEntity.instance;
            Vector3 dir = (player.transform.position - transform.position).normalized;
            float factor;
            if (dir.y > 0)
            {
                factor = Mathf.Lerp(HorizontalImpulse, UpwardImpulse, dir.y);
            }
            else
            {
                factor = Mathf.Lerp(HorizontalImpulse, DownwardImpulse, -dir.y);
            }
            player.health.Hit(Damage, new Impulse(dir * factor, 0.25f));
        }
    }

    public void Inactivate()
    {
        exploded = true;
    }

    public void Finish()
    {
        Destroy(gameObject);
    }
}
