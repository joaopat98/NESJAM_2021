using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownGunBullet : MonoBehaviour
{
    Vector3 direction;
    [SerializeField] LayerMask HitMask;
    [SerializeField] LayerMask DestroyMask;
    [SerializeField] float Speed;

    [SerializeField] float boostHeight;

    void Awake(){ this.direction = new Vector3(0, -1, 0); }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (HitMask == (HitMask | (1 << collider.gameObject.layer)))
        {
            //hit target
        }
        if (DestroyMask == (DestroyMask | (1 << collider.gameObject.layer)))
        {
            Destroy(gameObject);
        }
    }

    public float GetBoostHeight()
    {
        return boostHeight;
    }

    void Update()
    {
        transform.Translate(direction.normalized * Speed * Time.deltaTime, Space.World);
    }
}
