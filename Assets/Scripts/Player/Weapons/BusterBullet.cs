using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraSpaceObject))]
public class BusterBullet : MonoBehaviour
{
    Vector3 direction;
    [SerializeField] LayerMask HitMask;
    [SerializeField] LayerMask DestroyMask;
    [SerializeField] float Speed;

    private BoxCollider2D collider;

    [Header("Mid Charge Shot")]
    [SerializeField] private float midChargeSpeedMod;
    [SerializeField] private float midChargeScaleMod;

    [Header("Full Charge Shot")]
    [SerializeField] private float fullChargeSpeedMod;
    [SerializeField] private float fullChargeScaleMod;

    void Awake() { collider = GetComponent<BoxCollider2D>(); }

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
        ChangeBulletScale(1 + midChargeScaleMod);
        ChangeBulletSpeed(1 + midChargeSpeedMod);
    }

    public void FullCharge()
    {
        ChangeBulletScale(1 + fullChargeScaleMod);
        ChangeBulletSpeed(1 + fullChargeSpeedMod);
    }

    private void ChangeBulletSpeed(float modifier) { Speed *= modifier; }

    private void ChangeBulletScale(float modifier)
    {
        transform.localScale = new Vector3(modifier, modifier, modifier);

        Vector2 S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        collider.size = S;
    }

}
