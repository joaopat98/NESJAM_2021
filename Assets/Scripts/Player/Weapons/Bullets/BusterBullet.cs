using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraSpaceObject))]
public class BusterBullet : Bullet
{
    Vector3 direction;
    [SerializeField] LayerMask HitMask;
    [SerializeField] LayerMask DestroyMask;
    [SerializeField] float Speed;

    private BoxCollider2D collider;
    private SpriteRenderer spriteRenderer;

    [Header("Mid Charge Shot")]
    [SerializeField] private float midChargeSpeedMod;
    [SerializeField] private Sprite midChargeSprite;

    [Header("Full Charge Shot")]
    [SerializeField] private float fullChargeSpeedMod;
    [SerializeField] private Sprite fullChargeSprite;

    void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (HitMask.HasLayer(collider.gameObject.layer))
        {
            //hit target
        }
        if (DestroyMask.HasLayer(collider.gameObject.layer))
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

    public void MidCharge()
    {
        ChangeBulletSprite(midChargeSprite);
        ChangeBulletSpeed(1 + midChargeSpeedMod);
    }

    public void FullCharge()
    {
        ChangeBulletSprite(fullChargeSprite);
        ChangeBulletSpeed(1 + fullChargeSpeedMod);
    }

    private void ChangeBulletSpeed(float modifier) { Speed *= modifier; }

    private void ChangeBulletSprite(Sprite newSprite)
    {
        spriteRenderer.sprite = newSprite;
    }

}
