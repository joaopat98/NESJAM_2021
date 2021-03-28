using UnityEngine;

public class BossBomb : ThrowableBomb
{
    [SerializeField] GameObject ExplosionPrefab;
    protected override void Explode()
    {
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
    }
}