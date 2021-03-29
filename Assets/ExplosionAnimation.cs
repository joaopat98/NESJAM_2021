using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAnimation : MonoBehaviour
{
    public float animationTime;

    void Start()
    {
        Invoke("Die", animationTime);
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
