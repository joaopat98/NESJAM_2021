using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] int Damage;
    [SerializeField] bool InstaKill;
    [SerializeField] float HorizontalImpulse = 3;
    [SerializeField] float UpwardImpulse = 5;
    [SerializeField] float DownwardImpulse = 1;

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerEntity player = collision.collider.GetComponent<PlayerEntity>();
            if (InstaKill)
                player.health.Hit(player.health.CurrentHealth);
            else
            {
                Vector2 normal = Vector2.zero;
                for (int i = 0; i < collision.contactCount; i++)
                {
                    normal -= collision.GetContact(i).normal;
                }
                normal /= collision.contactCount;
                float factor;
                if (normal.y > 0)
                {
                    factor = Mathf.Lerp(HorizontalImpulse, UpwardImpulse, normal.y);
                }
                else
                {
                    factor = Mathf.Lerp(HorizontalImpulse, DownwardImpulse, -normal.y);
                }
                player.health.Hit(Damage, new Impulse(normal * factor, 0.25f));
            }
            //player.movement.Jump();
        }
    }
}
