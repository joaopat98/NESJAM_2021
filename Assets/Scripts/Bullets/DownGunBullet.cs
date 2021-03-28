using UnityEngine;

public class DownGunBullet : PlayerBullet
{
    DownGun spawner;
    public void Init(Vector2 direction, DownGun spawner)
    {
        this.direction = direction;
        this.spawner = spawner;
    }

    void OnDestroy()
    {
        spawner.OnShotDestroyed();
    }
}