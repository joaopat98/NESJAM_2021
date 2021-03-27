using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class NapalmBomb: Bullet
{
    public float detonationTime = 2f;
    public GameObject bulletPrefab;

    private void Start()
    {
        Invoke("Detonate", detonationTime);
    }

    void Detonate()
    {
        //TODO play explosion animation
        var bullet1 = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
        var bullet2 = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
        var bullet3 = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
        var bullet4 = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
        bullet1.Init(new Vector2(-1, -1));
        bullet2.Init(new Vector2(1, -1));
        bullet3.Init(new Vector2(-0.5f, -1));
        bullet4.Init(new Vector2(0.5f, -1));
        Destroy(this.gameObject);
    }
}
