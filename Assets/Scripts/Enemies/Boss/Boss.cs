using UnityEngine;

public class Boss : EnemyBase<Boss>
{
    [System.Serializable]
    public class Light
    {
        [System.Serializable]
        public class First
        {
            public GameObject BombPrefab;
            public int NumBombs;
            public float TimeBetweenBombs;
            public float Cooldown;
            public float TeleportTime;
            public Transform[] Teleports;
            [HideInInspector] public int currentTeleport;
        }
        public First first;
    }
    new public Light light;

    protected override void Start()
    {
        base.Start();
        transform.position = light.first.Teleports[0].position;
        state = BossFirstPhaseLightIdle.Create(this);
    }
}